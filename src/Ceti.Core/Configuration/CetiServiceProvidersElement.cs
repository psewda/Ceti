using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Configuration
{
    public class CetiServiceProvidersElement : ConfigurationElement
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the execution service providers.
        /// </summary>
        [ConfigurationProperty("executionServiceProviders")]
        public CetiExecutionServiceProviderElementCollection ExecutionServiceProviders
        {
            get
            {
                return (CetiExecutionServiceProviderElementCollection)this["executionServiceProviders"];
            }
            set
            {
                this["executionServiceProviders"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the data service providers.
        /// </summary>
        [ConfigurationProperty("dataServiceProviders")]
        public CetiDataServiceProviderElementCollection DataServiceProviders
        {
            get
            {
                return (CetiDataServiceProviderElementCollection)this["dataServiceProviders"];
            }
            set
            {
                this["dataServiceProviders"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the interception service providers.
        /// </summary>
        [ConfigurationProperty("interceptionServiceProviders")]
        public CetiInterceptionServiceProviderElementCollection InterceptionServiceProviders
        {
            get
            {
                return (CetiInterceptionServiceProviderElementCollection)this["interceptionServiceProviders"];
            }
            set
            {
                this["interceptionServiceProviders"] = value;
            }
        }

        #endregion
    }   
}
