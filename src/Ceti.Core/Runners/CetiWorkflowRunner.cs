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

                // Process execution service providers on exception occurrence
                this.ProcessExecutionService(exp2);

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

            // Process data service providers
            this.ProcessDataService();

            // Process execution service providers on workflow start
            this.setContext(this.Driver.ExecutionContext, null, CetiExecutionStage.WorkflowStart);
            this.ProcessExecutionService(this.Driver.ExecutionContext);

            // Initialize the workflow by calling 'Initialize' override
            this.workflow.Initialize();

            // Validate the workflow by calling 'Validate' override
            this.workflow.IsValid = this.workflow.Validate();

            // Get the entry point agent
            var agent = workflow.GetEntryPointAgent();

            // Invoke the entry point agent
            var selector = this.invokeAgent(agent, true);

            // Invoke other agents in the workflow
            while (selector != null && selector.NextAgent != null)
            {
                selector = this.invokeAgent(selector.NextAgent, false);
            }
            
            // Cleanup the workflow by calling 'Cleanup' override
            this.workflow.Cleanup();

            // Process execution service providers on workflow end
            this.setContext(this.Driver.ExecutionContext, null, CetiExecutionStage.WorkflowEnd);
            this.ProcessExecutionService(this.Driver.ExecutionContext);

            // Return the output data
            return this.workflow.OutputData;
        }

        /// <summary>
        /// Invokes the specified component agent.
        /// </summary>
        /// <param name="agent">The agent to be invoked.</param>
        /// <param name="isEntryPoint">The boolean flag for entry point agent.</param>
        /// <returns>The agent selector pointing to next agent.</returns>
        private CetiAgentSelector invokeAgent(Func<CetiComponentRunnerInfo, CetiAgentSelector> agent, bool isEntryPoint)
        {
            // Create agent info instance
            var agentInfo = new CetiAgentInfo(CetiAgentType.Component, agent.Method, isEntryPoint);

            // Process execution service providers on component agent start
            this.setContext(this.Driver.ExecutionContext, agentInfo, CetiExecutionStage.ComponentAgentStart);
            this.ProcessExecutionService(this.Driver.ExecutionContext);

            // Invoke the component agent
            var selector = agent.Invoke(new CetiComponentRunnerInfo(this.Driver));

            // Process execution service providers on component agent end
            this.setContext(this.Driver.ExecutionContext, agentInfo, CetiExecutionStage.ComponentAgentEnd);
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
            context.Workflow = context.Workflow ?? new CetiWorkflowInfo(this.workflow);
            context.Component = null;
            context.Agent = agent;
            context.Task = null;
            context.Stage = stage;
            return context;
        }

        #endregion
    }
}
