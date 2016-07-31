using Ceti.Core.ServiceProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public class CetiServiceProviderInfo
    {
        #region Private Fields

        /// <summary>
        /// The driver instance.
        /// </summary>
        private CetiDriver driver;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class with specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        public CetiServiceProviderInfo(CetiDriver driver)
        {
            // Set driver instance
            this.driver = driver;

            // Set service provider repositories
            this.ExecutionService = new CetiServiceProviderRepository<ICetiExecutionService>(driver);
            this.DataService = new CetiServiceProviderRepository<ICetiDataService>(driver);
            this.InterceptionService = new CetiServiceProviderRepository<ICetiInterceptionService>(driver);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets execution service repository.
        /// </summary>
        public CetiServiceProviderRepository<ICetiExecutionService> ExecutionService { get; private set; }

        /// <summary>
        /// Gets data service repository.
        /// </summary>
        public CetiServiceProviderRepository<ICetiDataService> DataService { get; private set; }

        /// <summary>
        /// Gets interception service repository.
        /// </summary>
        public CetiServiceProviderRepository<ICetiInterceptionService> InterceptionService { get; private set; }

        #endregion
    }
}
