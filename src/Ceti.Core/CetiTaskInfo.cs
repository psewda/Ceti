using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public class CetiTaskInfo
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with specified parameters.
        /// </summary>
        /// <param name="method">The task method.</param>
        /// <param name="inputData">The task input data.</param>
        /// <param name="outputData">The task output data.</param>
        public CetiTaskInfo(MethodInfo method, CetiInputData inputData, CetiOutputData outputData)
        {
            this.Method = method;
            this.InputData = inputData;
            this.OutputData = outputData;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the task method.
        /// </summary>
        public MethodInfo Method { get; private set; }

        /// <summary>
        /// Gets the task input data.
        /// </summary>
        public CetiInputData InputData { get; private set; }

        /// <summary>
        /// Gets the task output data.
        /// </summary>
        public CetiOutputData OutputData { get; private set; }

        #endregion
    }
}
