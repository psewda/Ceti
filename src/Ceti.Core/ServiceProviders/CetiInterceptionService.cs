using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceti.Core.Support;
using System.Reflection;

namespace Ceti.Core.ServiceProviders
{
    public abstract class CetiInterceptionService<T> : ICetiInterceptionService where T : CetiInterceptionService<T>
    {
        #region Internal Properties

        /// <summary>
        /// Gets or sets the interception service queue.
        /// </summary>
        internal Queue<T> InterceptionServiceQueue { get; set; }

        #endregion
    }
}
