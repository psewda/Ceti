using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.ServiceProviders
{
    public interface ICetiInterceptionService : ICetiService
    {
        #region Properties

        /// <summary>
        /// Gets the interception type.
        /// </summary>
        CetiInterceptionType InterceptionType { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a boolean flag indicating if interception is required or not.
        /// </summary>
        /// <param name="context">The interception context data.</param>
        /// <returns>The boolean flag indicating if interception is required or not.</returns>
        bool IsRequired(CetiInterceptionContext context);

        /// <summary>
        /// Intercepts the specified task/activity object.
        /// </summary>
        /// <param name="inputData">The data for the interception object.</param>
        /// <returns>The result of the interception object.</returns>
        CetiOutputData Intercept(CetiInputData inputData);

        #endregion
    }
}
