using Ceti.Core.ServiceProviders;
using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // Get interception service queue
            var interceptionServiceInstances = this.Driver.ServiceProvider.InterceptionService.Instances;
            var interceptionServiceQueue = interceptionServiceInstances.CreateServiceQueue(this.jobService.GetType(), inputData);

            // Get interception service instance
            var interceptionService = interceptionServiceQueue.GetNextInterceptionService();

            // Check interception service instance is available
            CetiOutputData outputData = null;
            if(interceptionService != null)
            {
                // Run interception service by calling the 'Intercept' override
                interceptionService.SetQueue(interceptionServiceQueue, this.jobService);
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
    }
}
