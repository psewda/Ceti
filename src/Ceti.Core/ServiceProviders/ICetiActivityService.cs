using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.ServiceProviders
{
    public interface ICetiActivityService : ICetiService    
    {
        #region Methods

        /// <summary>
        /// Runs the activity service.
        /// </summary>
        /// <param name="inputData">The data for the activity service.</param>
        /// <returns>The result of the activity service.</returns>
        CetiOutputData Run(CetiInputData inputData);

        #endregion
    }
}
