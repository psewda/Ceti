using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public abstract class CetiWorkflow : CetiBaseObject 
    { 
    
    }
    
    public abstract class CetiWorkflow<TInput, TOutput> : CetiWorkflow where TInput : CetiInputData where TOutput : CetiOutputData
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
