using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.ServiceProviders
{
    public interface ICetiDataService : ICetiService
    {
        #region Methods

        /// <summary>
        /// Loads the global data from some data source.
        /// </summary>
        /// <param name="globalData">The existing global data.</param>
        /// <returns>The updated global data.</returns>
        CetiGlobalDataCollection LoadData(CetiGlobalDataCollection globalData);

        #endregion
    }
}
