using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.ServiceProviders
{
    public class CetiInterceptionContext
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="task">The task method instance.</param>
        /// <param name="inputData">The input data instance.</param>
        public CetiInterceptionContext(MethodInfo task, CetiInputData inputData)
        {
            this.Task = task;
            this.Job = null;
            this.InputData = inputData;
        }

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="job">The job type instance.</param>
        /// <param name="inputData">The input data instance.</param>
        public CetiInterceptionContext(Type job, CetiInputData inputData)
        {
            this.Task = null;
            this.Job = job;
            this.InputData = inputData;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the task method.
        /// </summary>
        public MethodInfo Task { get; private set; }

        /// <summary>
        /// Gets the job type.
        /// </summary>
        public Type Job { get; private set; }

        /// <summary>
        /// Gets the input data.
        /// </summary>
        public CetiInputData InputData { get; private set; }

        #endregion
    }
}
