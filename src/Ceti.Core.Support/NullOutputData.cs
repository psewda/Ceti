using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Support
{
    public class NullOutputData : CetiOutputData
    {
        #region Public Properties

        /// <summary>
        /// Gets new instance of null output data.
        /// </summary>
        public static NullOutputData Instance 
        { 
            get 
            { 
                return new NullOutputData(); 
            }
        }

        #endregion
    }
}
