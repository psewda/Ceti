using Ceti.Core.ServiceProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public abstract class CetiComponent : CetiBaseObject
    {
        #region Public Properties

        /// <summary>
        /// Gets the workflow associated with the component.
        /// </summary>
        public CetiWorkflowInfo Workflow { get; internal set; }

        #endregion
    }

    public abstract class CetiComponent<TInput, TOutput> : CetiComponent where TInput : CetiInputData where TOutput : CetiOutputData
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets input data.
        /// </summary>
        public new TInput InputData
        {
            get
            {
                return (TInput)base.InputData;
            }
            protected set
            {
                base.InputData = value;
            }
        }

        /// <summary>
        /// Gets or sets output data.
        /// </summary>
        public new TOutput OutputData
        {
            get
            {
                return (TOutput)base.OutputData;
            }
            protected set
            {
                base.OutputData = value;
            }
        }

        #endregion
    }
}
