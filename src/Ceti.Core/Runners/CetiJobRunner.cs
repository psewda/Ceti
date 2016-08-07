using Ceti.Core.ServiceProviders;
using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Ceti.Core.Runners
{
    public class CetiJobRunner : CetiRunner
    {
        #region Private Fields

        /// <summary>
        /// The job service to run.
        /// </summary>
        private ICetiJobService jobService;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        /// <param name="jobService">The job service to run.</param>
        public CetiJobRunner(CetiDriver driver, ICetiJobService jobService) : base(driver)
        {
            this.jobService = jobService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the specified job service.
        /// </summary>
        /// <param name="inputData">The data for the running job service.</param>
        /// <returns>The result of the running job service.</returns>
        public override CetiOutputData Run(CetiInputData inputData)
        {
            // Create interception service queue
            var interceptionServiceInstances = this.Driver.ServiceProvider.InterceptionService.Instances;
            var interceptionServiceQueue = this.createQueue(interceptionServiceInstances, inputData);

            // Get interception service instance
            var interceptionService = interceptionServiceQueue.Dequeue<CetiJobInterceptionService>();

            // Check interception service instance is available
            CetiOutputData outputData = null;
            if(interceptionService != null)
            {
                // Run interception service by calling the 'Intercept' override
                interceptionService.InterceptionServiceQueue = interceptionServiceQueue;
                interceptionService.JobService = this.jobService;
                outputData = interceptionService.Intercept(inputData);
            }
            else
            {
                // Run original job
                outputData = this.jobService.Run(inputData);
            }

            // Return the output data
            return outputData;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates interception service queue from the specified interception services.
        /// </summary>
        /// <param name="serviceInstances">The interception service instances.</param>
        /// <param name="inputData">The input data for interception.</param>
        /// <returns>The queue having interception service instances.</returns>
        private Queue<CetiJobInterceptionService> createQueue(List<ICetiInterceptionService> interceptionServices, CetiInputData inputData)
        {
            // Create interception service queue
            var queue = new Queue<CetiJobInterceptionService>();

            // Add interception service instances in the queue
            if (interceptionServices != null)
            {
                foreach (var interceptionService in interceptionServices)
                {
                    var jobInterceptionService = interceptionService as CetiJobInterceptionService;
                    if (jobInterceptionService != null)
                    {
                        if(jobInterceptionService.IsRequired(this.jobService.GetType().GetTypeInfo(), inputData))
                        {
                            queue.Enqueue(jobInterceptionService);
                        }
                    }
                }
            }

            // Return the queue
            return queue;
        }

        #endregion
    }
}
