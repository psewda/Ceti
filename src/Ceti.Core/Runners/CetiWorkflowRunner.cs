using Ceti.Core.ServiceProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceti.Core.Support;
using System.Reflection;
using Ceti.Core.Exceptions;


namespace Ceti.Core.Runners
{
    public class CetiWorkflowRunner : CetiRunner
    {
        #region Private Fields

        /// <summary>
        /// The workflow to run.
        /// </summary>
        private CetiWorkflow workflow;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        /// <param name="workflow">The workflow to run.</param>
        public CetiWorkflowRunner(CetiDriver driver, CetiWorkflow workflow) : base(driver)
        {
            this.workflow = workflow;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the specified workflow.
        /// </summary>
        /// <param name="inputData">The data for the running workflow.</param>
        /// <returns>The result of the running workflow.</returns>
        public override CetiOutputData Run(CetiInputData inputData)
        {
            try
            {
                // Run the workflow by calling internal method
                return this.runInternal(inputData);
            }
            catch(Exception exp)
            {
                // Build the exception instance
                var msg = "Unhandled exception occured while processing."; // TODO :: Add appropriate message
                var exp2 = new CetiException(msg, exp);

                // Invoke 'OnException' method of execution service providers
                this.invokeOnException(exp2);

                // Invoke the global exception handler
                var status = false;
                if (this.Driver.ExceptionHandler != null)
                {
                    status = this.Driver.ExceptionHandler.Invoke(exp2);
                }

                // Check the exception handler status
                if(status)
                {
                    return this.workflow.OutputData;
                }
                else
                {
                    throw;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Runs the specified workflow.
        /// </summary>
        /// <param name="inputData">The data for the running workflow.</param>
        /// <returns>The result of the running workflow.</returns>
        private CetiOutputData runInternal(CetiInputData inputData)
        {
            // Set driver instance in call context
            CetiDriver.LocalDriver = CetiDriver.LocalDriver ?? this.Driver;

            // Set driver instance for the workflow
            this.workflow.Driver = this.Driver;

            // Set input data for the workflow
            this.workflow.InputData = inputData;

            // Invoke 'LoadData' method of data service providers
            this.invokeLoadData();

            // Invoke 'OnExecution' method of execution service providers on workflow start
            this.setContext(this.Driver.ExecutionContext, null, CetiExecutionStage.WorkflowStart);
            this.InvokeOnExecution(this.Driver.ExecutionContext);

            // Initialize the workflow by calling 'Initialize' override
            this.workflow.Initialize();

            // Validate the workflow by calling 'Validate' override
            this.workflow.IsValid = this.workflow.Validate();

            // Get the entry point agent
            var agent = this.getEntryPointAgent(this.workflow);

            // Invoke the entry point agent
            var selector = this.invokeAgent(agent, true);

            // Invoke other agents in the workflow
            while (selector != null && selector.NextAgent != null)
            {
                selector = this.invokeAgent(selector.NextAgent, false);
            }
            
            // Cleanup the workflow by calling 'Cleanup' override
            this.workflow.Cleanup();

            // Invoke 'OnExecution' method of execution service providers on workflow end
            this.setContext(this.Driver.ExecutionContext, null, CetiExecutionStage.WorkflowEnd);
            this.InvokeOnExecution(this.Driver.ExecutionContext);

            // Return the output data
            return this.workflow.OutputData;
        }

        /// <summary>
        /// Invokes the specified agent.
        /// </summary>
        /// <param name="agent">The agent to be invoked.</param>
        /// <param name="isEntryPoint">The boolean flag for entry point agent.</param>
        /// <returns>The agent selector pointing to next agent.</returns>
        private CetiAgentSelector invokeAgent(Func<CetiAgentSelector> agent, bool isEntryPoint)
        {
            // Create agent info instance
            var agentInfo = new CetiAgentInfo(agent.Method, isEntryPoint);

            // Invoke 'OnExecution' method of execution service providers on agent start
            this.setContext(this.Driver.ExecutionContext, agentInfo, CetiExecutionStage.AgentStart);
            this.InvokeOnExecution(this.Driver.ExecutionContext);

            // Invoke the agent method
            var selector = agent.Invoke();

            // Invoke 'OnExecution' method of execution service providers on agent end
            this.setContext(this.Driver.ExecutionContext, agentInfo, CetiExecutionStage.AgentEnd);
            this.InvokeOnExecution(this.Driver.ExecutionContext);

            // Return the selector pointing to next agent
            return selector;
        }

        /// <summary>
        /// Set the execution context with specified parameters.
        /// </summary>
        /// <param name="context">The execution context instance.</param>
        /// <param name="agent">The agent info instance.</param>
        /// <param name="stage">The execution stage.</param>
        /// <returns>The updated execution context instance.</returns>
        private CetiExecutionContext setContext(CetiExecutionContext context, CetiAgentInfo agent, CetiExecutionStage stage)
        {
            context.Workflow = context.Workflow ?? new CetiWorkflowInfo(this.workflow);
            context.Agent = agent;
            context.Task = null;
            context.Stage = stage;
            return context;
        }

        /// <summary>
        /// Get the entry point agent from the specified workflow.
        /// </summary>
        /// <param name="workflow">The workflow having agents.</param>
        /// <returns>The entry point agent delegate.</returns>
        private Func<CetiAgentSelector> getEntryPointAgent(CetiWorkflow workflow)
        {
            // Check the method is marked with entry point attribute
            Func<MethodInfo, bool> isEntryPoint = (mi) =>
            {
                return mi.GetCustomAttribute(typeof(CetiEntryPointAttribute)) != null ? true : false;
            };

            // Check the method has valid signature
            Func<MethodInfo, bool> isSignValid = (mi) =>
            {
                if (mi.GetParameters().Length == 0)
                {
                    if (mi.ReturnType == typeof(CetiAgentSelector))
                    {
                        return true;
                    }
                }
                return false;
            };

            // Gets all entry point methods
            var entryPointMethods = workflow.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(mi => isEntryPoint(mi))
                .Where(mi => isSignValid(mi))
                .ToList();

            // Check there is only single entry point method
            if (entryPointMethods.Count == 1)
            {
                // Create agent delegate from the method
                var agentDelegate = entryPointMethods[0].CreateDelegate(typeof(Func<CetiAgentSelector>), workflow);
                return (Func<CetiAgentSelector>)agentDelegate;
            }
            else
            {
                // Build exception message based on entry point method count
                var message = string.Empty;
                if (entryPointMethods.Count == 0)
                {
                    message = "The entrypoint agent not found.";
                }
                else
                {
                    message = "There are multiple entrypoint agents.";
                }

                // Throw exception if zero or multiple entry point methods
                throw new CetiException(message); // TODO :: throw more specific exception
            }
        }

        /// <summary>
        /// Invokes 'OnException' method of all execution service providers.
        /// </summary>
        /// <param name="exception">The exception instance.</param>
        private void invokeOnException(CetiException exception)
        {
            var executionServiceProviders = this.Driver.ServiceProvider.ExecutionService.Instances;
            if (executionServiceProviders != null && executionServiceProviders.Count > 0)
            {
                foreach (var executionServiceProvider in executionServiceProviders)
                {
                    executionServiceProvider.OnException(exception);
                }
            }
        }

        /// <summary>
        /// Invokes 'LoadData' method of all data service providers.
        /// </summary>
        private void invokeLoadData()
        {
            var dataServiceProviders = this.Driver.ServiceProvider.DataService.Instances;
            if (dataServiceProviders != null && dataServiceProviders.Count > 0)
            {
                foreach (var dataServiceProvider in dataServiceProviders)
                {
                    this.Driver.SetGlobalData(dataServiceProvider.LoadData(this.Driver.GlobalData));
                }
            }
        }

        #endregion
    }
}
