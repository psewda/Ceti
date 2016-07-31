using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Support
{
    public class TryResult<T>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="status">The status value.</param>
        /// <param name="data">The data value.</param>
        public TryResult(bool status, T data)
        {
            this.Status = status;
            this.Data = data;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the status.
        /// </summary>
        public bool Status { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public T Data { get; private set; }

        #endregion
    }
}
