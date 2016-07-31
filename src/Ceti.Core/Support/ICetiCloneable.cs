using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Support
{
    public interface ICetiCloneable<T>
    {
        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>The new object copied from the current instance.</returns>
        T Clone();

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <param name="param">Any extra data which can be used for clonning the current instance.</param>
        /// <returns>The new object copied from the current instance.</returns>
        T Clone(object param);

        #endregion
    }
}
