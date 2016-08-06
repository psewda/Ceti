using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.ServiceProviders
{
    public class CetiExecutionContext
    {
        #region Public Properties

        /// <summary>
        /// Gets the context workflow.
        /// </summary>
        public CetiWorkflowInfo Workflow { get; internal set; }
        
        /// <summary>
        /// Gets the context agent.
        /// </summary>
        public CetiAgentInfo Agent { get; internal set; }

        /// <summary>
        /// Gets the context task.
        /// </summary>
        public CetiTaskInfo Task { get; internal set; }

        /// <summary>
        /// Gets the context stage.
        /// </summary>
        public CetiExecutionStage Stage { get; internal set; }

        #endregion
    }
}
