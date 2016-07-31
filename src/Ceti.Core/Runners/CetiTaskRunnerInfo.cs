using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Runners
{
    public class CetiTaskRunnerInfo : CetiRunnerInfo
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        public CetiTaskRunnerInfo(CetiDriver driver) : base(driver)
        {
            
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified task in the runner.
        /// </summary>
        /// <param name="task">The task to run.</param>
        /// <returns>The task runner to run the task.</returns>
        public CetiTaskRunner Task(Func<CetiInputData, CetiOutputData> task)
        {
            return new CetiTaskRunner(this.Driver, task);
        }

        #endregion
    }
}
