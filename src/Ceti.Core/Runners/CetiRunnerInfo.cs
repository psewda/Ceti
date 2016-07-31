using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Runners
{
    public abstract class CetiRunnerInfo
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        public CetiRunnerInfo(CetiDriver driver)
        {
            this.Driver = driver;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the driver instance.
        /// </summary>
        protected CetiDriver Driver { get; private set; }

        #endregion
    }
}
