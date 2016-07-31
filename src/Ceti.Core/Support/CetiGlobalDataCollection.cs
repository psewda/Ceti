using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Ceti.Core.Support;

namespace Ceti.Core.Support
{
    public class CetiGlobalDataCollection : ReadOnlyDictionary<string, string>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with specified parameters.
        /// </summary>
        /// <param name="globalData">The global data collection having key/value pairs.</param>
        public CetiGlobalDataCollection(Dictionary<string, string> globalData) : base(globalData)
        {
            
        }

        #endregion
    }
}
