using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.ServiceProviders
{
    public interface ICetiChannelService : ICetiService
    {
        #region Methods

        /// <summary>
        /// Runs the channel service.
        /// </summary>
        /// <param name="inputData">The data for the channel service.</param>
        /// <returns>The result of the channel service.</returns>
        CetiOutputData Run(CetiInputData inputData);

        #endregion
    }
}
