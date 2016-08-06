using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.ServiceProviders
{
    public interface ICetiJobService : ICetiService    
    {
        #region Methods

        /// <summary>
        /// Runs the job service.
        /// </summary>
        /// <param name="inputData">The data for the job service.</param>
        /// <returns>The result of the job service.</returns>
        CetiOutputData Run(CetiInputData inputData);

        #endregion
    }
}
