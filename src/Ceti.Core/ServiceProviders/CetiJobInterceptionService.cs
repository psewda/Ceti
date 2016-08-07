using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ceti.Core.Support;

namespace Ceti.Core.ServiceProviders
{
    public abstract class CetiJobInterceptionService : CetiInterceptionService<CetiJobInterceptionService>
    {
        #region Internal Properties

        /// <summary>
        /// Gets or sets the job service to intercept.
        /// </summary>
        internal ICetiJobService JobService { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a boolean flag indicating if interception is required or not.
        /// </summary>
        /// <param name="jobService">The job service to intercept.</param>
        /// <param name="inputData">The input data for the job service.</param>
        /// <returns>The boolean flag indicating if interception is required or not.</returns>
        public abstract bool IsRequired(TypeInfo jobService, CetiInputData inputData);

        /// <summary>
        /// Intercepts the specified job service.
        /// </summary>
        /// <param name="inputData">The input data for the job service.</param>
        /// <returns>The result of the intercepted job service.</returns>
        public virtual CetiOutputData Intercept(CetiInputData inputData)
        {
            // Get next interception service instance from the queue
            var interceptionService = this.InterceptionServiceQueue.Dequeue<CetiJobInterceptionService>();

            // Check the interception service instance is available
            if (interceptionService != null)
            {
                // Run interception service by calling the 'Intercept' override
                interceptionService.InterceptionServiceQueue = this.InterceptionServiceQueue;
                interceptionService.JobService = this.JobService;
                return interceptionService.Intercept(inputData);
            }
            else
            {
                // Invoke the job service
                return this.JobService.Run(inputData);
            }
        }

        #endregion
    }
}
