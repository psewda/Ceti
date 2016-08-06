using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public class CetiWorkflowInfo
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="workflow">The workflow instance.</param>
        public CetiWorkflowInfo(CetiWorkflow workflow)
        {
            this.Id = workflow.Id;
            this.Name = workflow.Name;
            this.Driver = new CetiDriverInfo(workflow.Driver);
            this.Runtime = workflow.Runtime;
            this.InputData = workflow.InputData;
            this.OutputData = workflow.OutputData;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the workflow id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the workflow name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the driver instance.
        /// </summary>
        public CetiDriverInfo Driver { get; private set; }

        /// <summary>
        /// Gets the runtime data collection.
        /// </summary>
        public CetiDictionary<string, object> Runtime { get; private set; }

        /// <summary>
        /// Gets the input data.
        /// </summary>
        public CetiInputData InputData { get; private set; }

        /// <summary>
        /// Gets the output data.
        /// </summary>
        public CetiOutputData OutputData { get; private set; }

        #endregion
    }
}
