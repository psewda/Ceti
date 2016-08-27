using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ceti.Core.Support;

namespace Ceti.Core.ServiceProviders
{
    public abstract class CetiChannelInterceptionService : CetiInterceptionService<CetiChannelInterceptionService>
    {
        #region Internal Properties

        /// <summary>
        /// Gets or sets the channel service to intercept.
        /// </summary>
        internal ICetiChannelService ChannelService { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a boolean flag indicating if interception is required or not.
        /// </summary>
        /// <param name="channelService">The channel service to intercept.</param>
        /// <param name="inputData">The input data for the channel service.</param>
        /// <returns>The boolean flag indicating if interception is required or not.</returns>
        public abstract bool IsRequired(TypeInfo channelService, CetiInputData inputData);

        /// <summary>
        /// Intercepts the specified channel service.
        /// </summary>
        /// <param name="inputData">The input data for the channel service.</param>
        /// <returns>The result of the intercepted channel service.</returns>
        public virtual CetiOutputData Intercept(CetiInputData inputData)
        {
            // Get next interception service instance from the queue
            var interceptionService = this.InterceptionServiceQueue.Dequeue<CetiChannelInterceptionService>();

            // Check the interception service instance is available
            if (interceptionService != null)
            {
                // Run interception service by calling the 'Intercept' override
                interceptionService.InterceptionServiceQueue = this.InterceptionServiceQueue;
                interceptionService.ChannelService = this.ChannelService;
                return interceptionService.Intercept(inputData);
            }
            else
            {
                // Invoke the channel service
                return this.ChannelService.Run(inputData);
            }
        }

        #endregion
    }
}
