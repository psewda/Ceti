using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceti.Core.Support;

namespace Ceti.Core.ServiceProviders
{
    public abstract class CetiInterceptionService : ICetiInterceptionService
    {
        #region Private Fields

        /// <summary>
        /// The queue having interception service instances.
        /// </summary>
        private Queue<CetiInterceptionService> queue;

        /// <summary>
        /// The activity service instance.
        /// </summary>
        private ICetiActivityService activityService;

        /// <summary>
        /// The task delegate instance.
        /// </summary>
        private Func<CetiInputData, CetiOutputData> task;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the interception type.
        /// </summary>
        public abstract CetiInterceptionType InterceptionType { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a boolean flag indicating if interception is required or not.
        /// </summary>
        /// <param name="context">The interception context data.</param>
        /// <returns>The boolean flag indicating if interception is required or not.</returns>
        public abstract bool IsRequired(CetiInterceptionContext context);

        /// <summary>
        /// Intercepts the specified task/activity object.
        /// </summary>
        /// <param name="inputData">The data for the interception object.</param>
        /// <returns>The result of the interception object.</returns>
        public virtual CetiOutputData Intercept(CetiInputData inputData)
        {
            // Get next interception service instance from the queue
            var interceptionService = this.queue.GetNextInterceptionService();

            // Check the interception service instance is available
            if (interceptionService != null)
            {
                // Set the queue in the next interception service instance
                switch(this.InterceptionType)
                {
                    case CetiInterceptionType.Task:
                        interceptionService.SetQueue(this.queue, this.task);
                        break;

                    case CetiInterceptionType.Activity:
                        interceptionService.SetQueue(this.queue, this.activityService);
                        break;
                }

                // Run interception service by calling the 'Intercept' override
                return interceptionService.Intercept(inputData);
            }
            else
            {
                // Invoke the task/activity
                return this.InterceptionType == CetiInterceptionType.Task
                    ? this.task.Invoke(inputData)
                    : this.activityService.Run(inputData);
            }
        }

        #endregion

        #region Internal Operations

        /// <summary>
        /// Sets the specified queue having interception service instances.
        /// </summary>
        /// <param name="queue">The queue having interception service instances.</param>
        /// <param name="task">The task for interception.</param>
        internal void SetQueue(Queue<CetiInterceptionService> queue, Func<CetiInputData, CetiOutputData> task)
        {
            this.queue = queue;
            this.activityService = null;
            this.task = task;
        }

        /// <summary>
        /// Sets the specified queue having interception service instances.
        /// </summary>
        /// <param name="queue">The queue having interception service instances.</param>
        /// <param name="activity">The activity for interception.</param>
        internal void SetQueue(Queue<CetiInterceptionService> queue, ICetiActivityService activity)
        {
            this.queue = queue;
            this.activityService = activity;
            this.task = null;
        }

        #endregion
    }
}
