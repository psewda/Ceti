using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Runners
{
    public class CetiComponentRunnerInfo : CetiRunnerInfo
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        public CetiComponentRunnerInfo(CetiDriver driver) : base(driver)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified component in the runner.
        /// </summary>
        /// <param name="component">The component to run.</param>
        /// <returns>The component runner to run the component.</returns>
        public CetiComponentRunner Component(CetiComponent component)
        {
            return new CetiComponentRunner(this.Driver, component);
        }

        #endregion
    }
}
