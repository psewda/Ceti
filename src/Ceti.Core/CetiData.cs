using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ceti.Core
{
    public abstract class CetiData
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class without parameters.
        /// </summary>
        public CetiData()
        {
            this.Values = new CetiDictionary<string, string>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the values collection.
        /// </summary>
        public CetiDictionary<string, string> Values { get; private set; }

        #endregion
    }
}
