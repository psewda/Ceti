using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.ServiceProviders
{
    public enum CetiExecutionStage
    {
        #region Enum Members

        /// <summary>
        /// The workflow start.
        /// </summary>
        WorkflowStart,

        /// <summary>
        /// The workflow end.
        /// </summary>
        WorkflowEnd,

        /// <summary>
        /// The component agent start.
        /// </summary>
        ComponentAgentStart,

        /// <summary>
        /// The component agent end.
        /// </summary>
        ComponentAgentEnd,

        /// <summary>
        /// The component start.
        /// </summary>
        ComponentStart,

        /// <summary>
        /// The component end.
        /// </summary>
        ComponentEnd,

        /// <summary>
        /// The task agent start.
        /// </summary>
        TaskAgentStart,

        /// <summary>
        /// The task agent end.
        /// </summary>
        TaskAgentEnd,

        /// <summary>
        /// The task start.
        /// </summary>
        TaskStart,

        /// <summary>
        /// The task end.
        /// </summary>
        TaskEnd

        #endregion
    }
}
