using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ceti.Core.Support;

namespace Ceti.Core.ServiceProviders
{
    public abstract class CetiTaskInterceptionService : CetiInterceptionService<CetiTaskInterceptionService>
    {
        #region Internal Properties

        /// <summary>
        /// Gets or sets the task to intercept.
        /// </summary>
        internal Func<CetiInputData, CetiOutputData> Task { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a boolean flag indicating if interception is required or not.
        /// </summary>
        /// <param name="taskRepository">The repository class having task methods.</param>
        /// <param name="task">The task method to intercept.</param>
        /// <param name="inputData">The input data for the task.</param>
        /// <returns>The boolean flag indicating if interception is required or not.</returns>
        public abstract bool IsRequired(TypeInfo taskRepository, MethodInfo task, CetiInputData inputData);

        /// <summary>
        /// Intercepts the specified task.
        /// </summary>
        /// <param name="inputData">The input data for the task.</param>
        /// <returns>The result of the intercepted task.</returns>
        public virtual CetiOutputData Intercept(CetiInputData inputData)
        {
            // Get next interception service instance from the queue
            var interceptionService = this.InterceptionServiceQueue.Dequeue<CetiTaskInterceptionService>();

            // Check the interception service instance is available
            if (interceptionService != null)
            {
                // Run interception service by calling the 'Intercept' override
                interceptionService.InterceptionServiceQueue = this.InterceptionServiceQueue;
                interceptionService.Task = this.Task;
                return interceptionService.Intercept(inputData);
            }
            else
            {
                // Invoke the task
                return this.Task.Invoke(inputData);
            }
        }

        #endregion
    }
}
