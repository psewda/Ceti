using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public class CetiComponentInfo : CetiBaseObjectInfo
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="component">The component instance.</param>
        public CetiComponentInfo(CetiComponent component) : base(component)
        {
            this.Workflow = component.Workflow;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the workflow associated with the component.
        /// </summary>
        public CetiWorkflowInfo Workflow { get; private set; }

        #endregion
    }
}
