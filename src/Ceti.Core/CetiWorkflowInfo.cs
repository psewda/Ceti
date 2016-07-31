using Ceti.Core.Exceptions;
using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public class CetiWorkflowInfo : CetiBaseObjectInfo, ICetiCloneable<CetiWorkflowInfo>
    {
        #region Private Fields

        /// <summary>
        /// The workflow instance.
        /// </summary>
        private CetiWorkflow workflow;

        /// <summary>
        /// The data access flag.
        /// </summary>
        private bool isDataAccess;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="workflow">The workflow instance.</param>
        public CetiWorkflowInfo(CetiWorkflow workflow) : this(workflow, true)
        {
            
        }

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="workflow">The workflow instance.</param>
        /// <param name="isDataAccess">The boolean flag for data access.</param>
        public CetiWorkflowInfo(CetiWorkflow workflow, bool isDataAccess) : base(workflow)
        {
            this.workflow = workflow;
            this.isDataAccess = isDataAccess;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the runtime data collection.
        /// </summary>
        public override CetiDictionary<string, object> Runtime
        {
            get
            {
                if (this.isDataAccess)
                {
                    return base.Runtime;
                }
                else
                {
                    throw new CetiException("Invalid operation"); // TODO :: throw more specific exception
                }
            }
        }

        /// <summary>
        /// Gets the input data.
        /// </summary>
        public override CetiInputData InputData
        {
            get
            {
                if(this.isDataAccess)
                {
                    return base.InputData;
                }
                else
                {
                    throw new CetiException("Invalid operation"); // TODO :: throw more specific exception
                }
            }
        }

        /// <summary>
        /// Gets the output data.
        /// </summary>
        public override CetiOutputData OutputData
        {
            get
            {
                if (this.isDataAccess)
                {
                    return base.OutputData;
                }
                else
                {
                    throw new CetiException("Invalid operation"); // TODO :: throw more specific exception
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>The new object copied from the current instance.</returns>
        CetiWorkflowInfo ICetiCloneable<CetiWorkflowInfo>.Clone()
        {
            return new CetiWorkflowInfo(this.workflow);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <param name="param">Any extra data which can be used for clonning the current instance.</param>
        /// <returns>The new object copied from the current instance.</returns>
        CetiWorkflowInfo ICetiCloneable<CetiWorkflowInfo>.Clone(object param)
        {
            if (param != null && param.GetType() == typeof(bool))
            {
                return new CetiWorkflowInfo(this.workflow, bool.Parse(param.ToString()));
            }
            else
            {
                return new CetiWorkflowInfo(this.workflow);
            }
        }

        #endregion
    }
}
