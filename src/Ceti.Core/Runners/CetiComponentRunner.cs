using Ceti.Core.Exceptions;
using Ceti.Core.ServiceProviders;
using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Runners
{
    public class CetiComponentRunner : CetiRunner
    {
        #region Private Fields

        /// <summary>
        /// The component to run.
        /// </summary>
        private CetiComponent component;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        /// <param name="component">The component to run.</param>
        public CetiComponentRunner(CetiDriver driver, CetiComponent component) : base(driver)
        {
            this.component = component;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the specified component.
        /// </summary>
        /// <param name="inputData">The data for the running component.</param>
        /// <returns>The result of the running component.</returns>
        public override CetiOutputData Run(CetiInputData inputData)
        {
            try
            {
                // Run the component by calling internal method
                return this.runInternal(inputData);
            }
            catch(Exception exp)
            {
                if (!this.isWorkflowAvailable())
                {
                    // Build the exception instance
                    var msg = "Unhandled exception occured while processing."; // TODO :: Add appropriate message
                    var exp2 = new CetiException(msg, exp);

                    // Process execution service providers on exception occurrence
                    this.ProcessExecutionService(exp2);

                    // Invoke the global exception handler
                    var status = false;
                    if (this.Driver.ExceptionHandler != null)
                    {
                        status = this.Driver.ExceptionHandler.Invoke(exp2);
                    }

                    // Check the exception handler status
                    if (status)
                    {
                        return this.component.OutputData;
                    }
                    else
                    {
                        throw;
                    }
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
        /// Runs the specified component.
        /// </summary>
        /// <param name="inputData">The data for the running component.</param>
        /// <returns>The result of the running component.</returns>
        private CetiOutputData runInternal(CetiInputData inputData)
        {
            // Set driver instance in call context
            CetiDriver.LocalDriver = CetiDriver.LocalDriver ?? this.Driver;

            // Set driver instance for the component
            this.component.Driver = this.Driver;

            // Set workflow instance for the component
            var cloneable = this.Driver.ExecutionContext.Workflow as ICetiCloneable<CetiWorkflowInfo>;
            this.component.Workflow = cloneable.Clone(false);

            // Set input data for the component
            this.component.InputData = inputData;

            // Process data service providers
            if(!this.isWorkflowAvailable())
            {
                this.ProcessDataService();
            }
            
            // Process execution service providers on component start
            this.setContext(this.Driver.ExecutionContext, null, CetiExecutionStage.ComponentStart);
            this.ProcessExecutionService(this.Driver.ExecutionContext);

            // Initialize the component by calling 'Initialize' override
            this.component.Initialize();

            // Validate the component by calling 'Validate' override
            this.component.IsValid = this.component.Validate();

            // Get the entry point agent
            var agent = component.GetEntryPointAgent();

            // Invoke the entry point agent
            var selector = this.invokeAgent(agent, true);

            // Invoke other agents in the component
            while (selector != null && selector.NextAgent != null)
            {
                selector = this.invokeAgent(selector.NextAgent, false);
            }

            // Cleanup the component by calling 'Cleanup' override
            this.component.Cleanup();

            // Process execution service providers on component end
            this.setContext(this.Driver.ExecutionContext, null, CetiExecutionStage.ComponentEnd);
            this.ProcessExecutionService(this.Driver.ExecutionContext);

            // Return the output data
            return this.component.OutputData;
        }

        /// <summary>
        /// Invokes the specified task agent.
        /// </summary>
        /// <param name="agent">The agent to be invoked.</param>
        /// <param name="isEntryPoint">The boolean flag for entry point agent.</param>
        /// <returns>The agent selector pointing to next agent.</returns>
        private CetiAgentSelector invokeAgent(Func<CetiTaskRunnerInfo, CetiAgentSelector> agent, bool isEntryPoint)
        {
            // Create agent info instance
            var agentInfo = new CetiAgentInfo(CetiAgentType.Task, agent.Method, isEntryPoint);

            // Process execution service providers on component agent start
            this.setContext(this.Driver.ExecutionContext, agentInfo, CetiExecutionStage.TaskAgentStart);
            this.ProcessExecutionService(this.Driver.ExecutionContext);

            // Invoke the task agent
            var selector = agent.Invoke(new CetiTaskRunnerInfo(this.Driver));

            // Process execution service providers on component agent end
            this.setContext(this.Driver.ExecutionContext, agentInfo, CetiExecutionStage.TaskAgentEnd);
            this.ProcessExecutionService(this.Driver.ExecutionContext);

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
            context.Component = context.Component ?? new CetiComponentInfo(this.component);
            context.Agent = agent;
            context.Task = null;
            context.Stage = stage;
            return context;
        }

        /// <summary>
        /// Gets a boolean flag indicating if the component is part of any workflow.
        /// </summary>
        private bool isWorkflowAvailable()
        {
            return this.Driver.ExecutionContext.Workflow != null;
        }

        #endregion
    }
}
