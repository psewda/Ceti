using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Configuration
{
    public class CetiConfigurationSection : ConfigurationSection
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the service providers.
        /// </summary>
        [ConfigurationProperty("serviceProviders")]
        public CetiServiceProvidersElement ServiceProviders
        {
            get
            {
                return this["serviceProviders"] as CetiServiceProvidersElement;
            }
            set
            {
                this["serviceProviders"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the global data.
        /// </summary>
        [ConfigurationProperty("globalData")]
        public CetiGlobalDataElementCollection GlobalData
        {
            get
            {
                return this["globalData"] as CetiGlobalDataElementCollection;
            }
            set
            {
                this["globalData"] = value;
            }
        }

        #endregion
    }
}
