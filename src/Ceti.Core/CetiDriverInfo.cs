using Ceti.Core.ServiceProviders;
using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public class CetiDriverInfo
    {
        #region Private Fields

        /// <summary>
        /// The driver instance.
        /// </summary>
        private CetiDriver driver;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        public CetiDriverInfo(CetiDriver driver)
        {
            this.driver = driver;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets data service providers.
        /// </summary>
        public ReadOnlyCollection<ICetiDataService> DataServiceProviders
        {
            get
            {
                return this.driver.ServiceProvider.DataService.Instances.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets execution service providers.
        /// </summary>
        public ReadOnlyCollection<ICetiExecutionService> ExecutionServiceProviders
        {
            get
            {
                return this.driver.ServiceProvider.ExecutionService.Instances.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets interception service providers.
        /// </summary>
        public ReadOnlyCollection<ICetiInterceptionService> InterceptionServiceProviders
        {
            get
            {
                return this.driver.ServiceProvider.InterceptionService.Instances.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the global data collection.
        /// </summary>
        public CetiGlobalDataCollection GlobalData
        {
            get
            {
                return this.driver.GlobalData;
            }
        }

        #endregion
    }
}
