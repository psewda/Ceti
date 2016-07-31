using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Configuration
{
    public abstract class CetiBaseConfigurationElement : ConfigurationElement
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return this["value"].ToString();
            }
            set
            {
                this["value"] = value;
            }
        }

        #endregion
    }
}
