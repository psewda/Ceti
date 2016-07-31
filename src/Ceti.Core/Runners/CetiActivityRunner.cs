using Ceti.Core.ServiceProviders;
using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Runners
{
    public class CetiActivityRunner : CetiRunner
    {
        #region Private Fields

        /// <summary>
        /// The activity service to run.
        /// </summary>
        private ICetiActivityService activityService;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        /// <param name="activityService">The activity service to run.</param>
        public CetiActivityRunner(CetiDriver driver, ICetiActivityService activityService) : base(driver)
        {
            this.activityService = activityService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the specified activity service.
        /// </summary>
        /// <param name="inputData">The data for the running activity service.</param>
        /// <returns>The result of the running activity service.</returns>
        public override CetiOutputData Run(CetiInputData inputData)
        {
            // Get interception service queue
            var interceptionServiceInstances = this.Driver.ServiceProvider.InterceptionService.Instances;
            var interceptionServiceQueue = interceptionServiceInstances.CreateServiceQueue(this.activityService.GetType(), inputData);

            // Get interception service instance
            var interceptionService = interceptionServiceQueue.GetNextInterceptionService();

            // Check interception service instance is available
            CetiOutputData outputData = null;
            if(interceptionService != null)
            {
                // Run interception service by calling the 'Intercept' override
                interceptionService.SetQueue(interceptionServiceQueue, this.activityService);
                outputData = interceptionService.Intercept(inputData);
            }
            else
            {
                // Run original activity
                outputData = this.activityService.Run(inputData);
            }

            // Return the output data
            return outputData;
        }

        #endregion
    }
}
