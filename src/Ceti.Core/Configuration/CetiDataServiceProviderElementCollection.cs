using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Configuration
{
    public class CetiDataServiceProviderElementCollection : ConfigurationElementCollection
    {
        #region Public Properties

        /// <summary>
        /// Gets the collection type.
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }

        /// <summary>
        ///  Gets the element name.
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return "DataServiceProvider";
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Verifies the element name.
        /// </summary>
        /// <param name="elementName">The element name to be verified.</param>
        /// <returns>A boolean flag indicating the status of element name match.</returns>
        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals("DataServiceProvider", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Creates a new configuration element.
        /// </summary>
        /// <returns>The configuration element instance.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new CetiDataServiceProviderElement();
        }

        /// <summary>
        /// Gets the element key.
        /// </summary>
        /// <param name="element">The configuration element.</param>
        /// <returns>The element key.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CetiDataServiceProviderElement)element).Name;
        }

        #endregion
    }
}
