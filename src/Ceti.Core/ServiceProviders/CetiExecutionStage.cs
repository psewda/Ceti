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
        /// The agent start.
        /// </summary>
        AgentStart,

        /// <summary>
        /// The agent end.
        /// </summary>
        AgentEnd,

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
