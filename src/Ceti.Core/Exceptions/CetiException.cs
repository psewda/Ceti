using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Exceptions
{
    public class CetiException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class without parameters.
        /// </summary>
        public CetiException()
        {

        }

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CetiException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CetiException(string message, Exception innerException) : base(message, innerException)
        {

        }

        #endregion
    }
}
