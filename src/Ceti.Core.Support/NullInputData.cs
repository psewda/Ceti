using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Support
{
    public class NullInputData : CetiInputData
    {
        #region Public Properties

        /// <summary>
        /// Gets new instance of null input data.
        /// </summary>
        public static NullInputData Instance 
        { 
            get 
            { 
                return new NullInputData(); 
            }
        }

        #endregion
    }
}
