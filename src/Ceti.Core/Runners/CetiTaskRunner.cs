using Ceti.Core.ServiceProviders;
using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Runners
{
    public class CetiTaskRunner : CetiRunner
    {
        #region Private Fields

        /// <summary>
        /// The task to run.
        /// </summary>
        private Func<CetiInputData, CetiOutputData> task;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        /// <param name="task">The task to run.</param>
        public CetiTaskRunner(CetiDriver driver, Func<CetiInputData, CetiOutputData> task) : base(driver)
        {
            this.task = task;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the specified task.
        /// </summary>
        /// <param name="inputData">The data for the running task.</param>
        /// <returns>The result of the running task.</returns>
        public override CetiOutputData Run(CetiInputData inputData)
        {
            // Create task info object
            Func<CetiOutputData, CetiTaskInfo> createTaskInfo = (op) => 
            {
                return new CetiTaskInfo(this.task.Method, inputData, op);
            };

            // Process execution service providers on task start
            this.setContext(this.Driver.ExecutionContext, createTaskInfo(null), CetiExecutionStage.TaskStart);
            this.InvokeOnExecution(this.Driver.ExecutionContext);

            // Get interception service queue
            var interceptionServiceInstances = this.Driver.ServiceProvider.InterceptionService.Instances;
            var interceptionServiceQueue = interceptionServiceInstances.CreateServiceQueue(this.task.Method, inputData);

            // Get interception service instance
            var interceptionService = interceptionServiceQueue.GetNextInterceptionService();

            // Check interception service instance is available
            CetiOutputData outputData = null;
            if(interceptionService != null)
            {
                // Run interception service by calling the 'Intercept' override
                interceptionService.SetQueue(interceptionServiceQueue, this.task);
                outputData = interceptionService.Intercept(inputData);
            }
            else
            {
                // Invoke original task
                outputData = this.task.Invoke(inputData);
            }

            // Process execution service providers on task end
            this.setContext(this.Driver.ExecutionContext, createTaskInfo(outputData), CetiExecutionStage.TaskEnd);
            this.InvokeOnExecution(this.Driver.ExecutionContext);

            // Return the output data
            return outputData;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Set the execution context with specified parameters.
        /// </summary>
        /// <param name="context">The execution context instance.</param>
        /// <param name="task">The task info instance.</param>
        /// <param name="stage">The execution stage.</param>
        /// <returns>The updated execution context instance.</returns>
        private CetiExecutionContext setContext(CetiExecutionContext context, CetiTaskInfo task, CetiExecutionStage stage)
        {
            context.Agent = null;
            context.Task = task;
            context.Stage = stage;
            return context;
        }

        #endregion
    }
}
