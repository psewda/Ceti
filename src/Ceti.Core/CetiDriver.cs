using Ceti.Core.Configuration;
using Ceti.Core.Exceptions;
using Ceti.Core.Runners;
using Ceti.Core.ServiceProviders;
using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Ceti.Core
{
    public sealed class CetiDriver
    {
        #region Private Constructors

        /// <summary>
        /// Initializes the class without parameters.
        /// </summary>
        private CetiDriver()
        {
            // Set execution context
            this.ExecutionContext = new CetiExecutionContext();

            // Set service provider
            this.ServiceProvider = new CetiServiceProviderInfo(this);
            
            // Load execution service providers
            var executionServiceProviders = this.getServiceProviders<ICetiExecutionService>().ToList();
            this.ServiceProvider.ExecutionService.SetServiceProviders(executionServiceProviders);

            // Load data service providers
            var dataServiceProviders = this.getServiceProviders<ICetiDataService>().ToList();
            this.ServiceProvider.DataService.SetServiceProviders(dataServiceProviders);

            // Load interception service providers
            var interceptionServiceProviders = this.getServiceProviders<ICetiInterceptionService>().ToList();
            this.ServiceProvider.InterceptionService.SetServiceProviders(interceptionServiceProviders);

            // Load global data
            this.GlobalData = new CetiGlobalDataCollection(CetiConfigurationReader.GlobalData);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the service provider info.
        /// </summary>
        public CetiServiceProviderInfo ServiceProvider { get; private set; }

        /// <summary>
        /// Gets the global data collection.
        /// </summary>
        public CetiGlobalDataCollection GlobalData { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates new instance of the CetiDriver class.
        /// </summary>
        /// <returns></returns>
        public static CetiDriver Instance()
        {
            return new CetiDriver();
        }

        /// <summary>
        /// Adds the specified component in the driver.
        /// </summary>
        /// <param name="component">The component to run.</param>
        /// <returns>The component runner to run the component.</returns>
        public CetiComponentRunner Component(CetiComponent component)
        {
            return new CetiComponentRunner(this, component);
        }

        /// <summary>
        /// Adds the specified workflow in the driver.
        /// </summary>
        /// <param name="workflow">The workflow to run.</param>
        /// <returns>The workflow runner to run the workflow.</returns>
        public CetiWorkflowRunner Workflow(CetiWorkflow workflow) 
        {
            return new CetiWorkflowRunner(this, workflow);
        }

        /// <summary>
        /// Sets exception handler in the driver.
        /// </summary>
        /// <param name="exceptionHandler">The exception handler delegate.</param>
        /// <returns>The driver instance.</returns>
        public CetiDriver Exception(Func<CetiException, bool> exceptionHandler)
        {
            this.ExceptionHandler = exceptionHandler;
            return this;
        }

        #endregion

        #region Internal Operations

        /// <summary>
        /// Gets or sets the driver instance in call context.
        /// </summary>
        internal static CetiDriver LocalDriver
        {
            get
            {
                return CallContext.LogicalGetData("local.driver") as CetiDriver;
            }
            set
            {
                CallContext.LogicalSetData("local.driver", value);
            }
        }

        /// <summary>
        /// Gets execution context.
        /// </summary>
        internal CetiExecutionContext ExecutionContext { get; private set; }

        /// <summary>
        /// Gets exception handler delegate.
        /// </summary>
        internal Func<CetiException, bool> ExceptionHandler { get; private set; }

        /// <summary>
        /// Sets the specified global data collection.
        /// </summary>
        /// <param name="globalData">The global data collection.</param>
        internal void SetGlobalData(CetiGlobalDataCollection globalData)
        {
            this.GlobalData = globalData;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets service providers of the specified type.
        /// </summary>
        /// <returns>The collection of service providers.</returns>
        private IEnumerable<T> getServiceProviders<T>() where T : ICetiService
        {
            // Get the service providers from the config file
            Dictionary<string, string> serviceProviders = null;
            if (typeof(T) == typeof(ICetiExecutionService))
            {
                serviceProviders = CetiConfigurationReader.ExecutionServiceProviders;
            }
            else if (typeof(T) == typeof(ICetiDataService))
            {
                serviceProviders = CetiConfigurationReader.DataServiceProviders;
            }
            else if (typeof(T) == typeof(ICetiInterceptionService))
            {
                serviceProviders = CetiConfigurationReader.InterceptionServiceProviders;
            }

            // Make sure service providers are valid and type compatible
            if(serviceProviders != null && serviceProviders.Count > 0)
            {
                foreach(var serviceProvider in serviceProviders.Values)
                {
                    this.checkType<T>(serviceProvider);
                    yield return (T)Activator.CreateInstance(Type.GetType(serviceProvider));
                }
            }
        }

        /// <summary>
        /// Checks the specified type with certain constraints.
        /// </summary>
        /// <param name="typeName">The assembly qualified type name.</param>
        private void checkType<T>(string typeName) where T : ICetiService
        {
            // Check type is not null/empty and is valid type
            if (string.IsNullOrEmpty(typeName) || Type.GetType(typeName) == null)
            {
                throw new CetiException("invalid type"); // TODO :: throw more specific exception
            }

            // Check type has implemented the required interface
            if(!typeof(T).IsAssignableFrom(Type.GetType(typeName)))
            {
                throw new CetiException("invalid type"); // TODO :: throw more specific exception
            }
        }

        #endregion
    }
}
