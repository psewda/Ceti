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
        /// <param name="componentAgent">The component agent delegate.</param>
        public CetiAgentSelector(Func<CetiComponentRunnerInfo, CetiAgentSelector> componentAgent)
        {
            this.NextAgent = (ri) => componentAgent((CetiComponentRunnerInfo)ri);
        }

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="taskAgent">The task agent delegate.</param>
        public CetiAgentSelector(Func<CetiTaskRunnerInfo, CetiAgentSelector> taskAgent)
        {
            this.NextAgent = (ri) => taskAgent((CetiTaskRunnerInfo)ri);
        }

        #endregion

        #region Publlic Properties

        /// <summary>
        /// Gets next component/task agent.
        /// </summary>
        public Func<CetiRunnerInfo, CetiAgentSelector> NextAgent { get; private set; }

        #endregion
    }
}
