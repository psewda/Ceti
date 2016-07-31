using Ceti.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.ServiceProviders
{
    public interface ICetiExecutionService : ICetiService
    {
        #region Methods

        /// <summary>
        /// Gets called at different stages in the execution flow.
        /// </summary>
        /// <param name="context">The execution context instance.</param>
        void OnExecution(CetiExecutionContext context);

        /// <summary>
        /// Gets called when any exception raised in the execution flow.
        /// </summary>
        /// <param name="exception">The base exception instance.</param>
        void OnException(CetiException exception);

        #endregion
    }
}
