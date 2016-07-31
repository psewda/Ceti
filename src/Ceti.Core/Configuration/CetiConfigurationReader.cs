using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Configuration
{
    public class CetiConfigurationReader
    {
        #region Public Properties

        /// <summary>
        /// Gets the execution service providers from the configuration file.
        /// </summary>
        public static Dictionary<string, string> ExecutionServiceProviders
        {
            get
            {
                return CetiConfigurationReader.getCetiConfigurationSection()
                    .ServiceProviders
                    .ExecutionServiceProviders
                    .OfType<CetiExecutionServiceProviderElement>()
                    .ToDictionary(kv => kv.Name, kv => kv.Value);
            }
        }

        /// <summary>
        /// Gets the data service providers from the configuration file.
        /// </summary>
        public static Dictionary<string, string> DataServiceProviders
        {
            get
            {
                return CetiConfigurationReader.getCetiConfigurationSection()
                    .ServiceProviders
                    .DataServiceProviders
                    .OfType<CetiDataServiceProviderElement>()
                    .ToDictionary(kv => kv.Name, kv => kv.Value);
            }
        }

        /// <summary>
        /// Gets the interception service providers from the configuration file.
        /// </summary>
        public static Dictionary<string, string> InterceptionServiceProviders
        {
            get
            {
                return CetiConfigurationReader.getCetiConfigurationSection()
                    .ServiceProviders
                    .InterceptionServiceProviders
                    .OfType<CetiInterceptionServiceProviderElement>()
                    .ToDictionary(kv => kv.Name, kv => kv.Value);
            }
        }

        /// <summary>
        /// Gets the global data from the configuration file.
        /// </summary>
        public static Dictionary<string, string> GlobalData
        {
            get
            {
                return CetiConfigurationReader.getCetiConfigurationSection()
                    .GlobalData
                    .OfType<CetiGlobalDataElement>()
                    .ToDictionary(kv => kv.Name, kv => kv.Value);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the ceti configuration section from the configuration file..
        /// </summary>
        /// <returns>The ceti configuration section.</returns>
        private static CetiConfigurationSection getCetiConfigurationSection()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            return (CetiConfigurationSection)config.Sections["cetiConfiguration"];
        }

        #endregion
    }
}
