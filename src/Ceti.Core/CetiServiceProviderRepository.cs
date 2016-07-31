using Ceti.Core.ServiceProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public class CetiServiceProviderRepository<T> where T : class, ICetiService
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
        public CetiServiceProviderRepository(CetiDriver driver)
        {
            this.driver = driver;
            this.Instances = new List<T>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the service provider instances.
        /// </summary>
        public List<T> Instances { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified service provider in the repository.
        /// </summary>
        /// <param name="serviceProvider">The service provider instance.</param>
        /// <returns>The driver instance.</returns>
        public CetiDriver Add(T serviceProvider)
        {
            this.Instances.Add(serviceProvider);
            return this.driver;
        }

        /// <summary>
        /// Clears the service provider repository.
        /// </summary>
        /// <returns>The driver instance.</returns>
        public CetiDriver Clear()
        {
            this.Instances.Clear();
            return this.driver;
        }

        #endregion

        #region Internal Operations

        /// <summary>
        /// Sets the specified service providers in the repository.
        /// </summary>
        /// <param name="serviceProviders">The list of service providers.</param>
        internal void SetServiceProviders(List<T> serviceProviders)
        {
            this.Instances = serviceProviders;
        }

        #endregion
    }
}
