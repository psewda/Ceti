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
    public class CetiChannelRunner : CetiRunner
    {
        #region Private Fields

        /// <summary>
        /// The channel service to run.
        /// </summary>
        private ICetiChannelService channelService;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        /// <param name="channelService">The channel service to run.</param>
        public CetiChannelRunner(CetiDriver driver, ICetiChannelService channelService) : base(driver)
        {
            this.channelService = channelService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the specified channel service.
        /// </summary>
        /// <param name="inputData">The data for the running channel service.</param>
        /// <returns>The result of the running channel service.</returns>
        public override CetiOutputData Run(CetiInputData inputData)
        {
            // Create interception service queue
            var interceptionServiceInstances = this.Driver.ServiceProvider.InterceptionService.Instances;
            var interceptionServiceQueue = this.createQueue(interceptionServiceInstances, inputData);

            // Get interception service instance
            var interceptionService = interceptionServiceQueue.Dequeue<CetiChannelInterceptionService>();

            // Check interception service instance is available
            CetiOutputData outputData = null;
            if(interceptionService != null)
            {
                // Run interception service by calling the 'Intercept' override
                interceptionService.InterceptionServiceQueue = interceptionServiceQueue;
                interceptionService.ChannelService = this.channelService;
                outputData = interceptionService.Intercept(inputData);
            }
            else
            {
                // Run original channel
                outputData = this.channelService.Run(inputData);
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
        private Queue<CetiChannelInterceptionService> createQueue(List<ICetiInterceptionService> interceptionServices, CetiInputData inputData)
        {
            // Create interception service queue
            var queue = new Queue<CetiChannelInterceptionService>();

            // Add interception service instances in the queue
            if (interceptionServices != null)
            {
                foreach (var interceptionService in interceptionServices)
                {
                    var channelInterceptionService = interceptionService as CetiChannelInterceptionService;
                    if (channelInterceptionService != null)
                    {
                        if (channelInterceptionService.IsRequired(this.channelService.GetType().GetTypeInfo(), inputData))
                        {
                            queue.Enqueue(channelInterceptionService);
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
