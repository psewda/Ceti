using Ceti.Core.Runners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public class CetiAgentSelector
    {
        #region Publlic Constructors

        /// <summary>
        /// Initializes the class without parameters.
        /// </summary>
        public CetiAgentSelector()
        {
            this.NextAgent = null;
        }

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="taskAgent">The task agent delegate.</param>
        public CetiAgentSelector(Func<CetiTaskRunnerInfo, CetiAgentSelector> taskAgent)
        {
            this.NextAgent = taskAgent;
        }

        #endregion

        #region Publlic Properties

        /// <summary>
        /// Gets next task agent.
        /// </summary>
        public Func<CetiTaskRunnerInfo, CetiAgentSelector> NextAgent { get; private set; }

        #endregion
    }
}
