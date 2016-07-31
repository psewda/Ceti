using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public abstract class CetiBaseObjectInfo
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="baseObject">The base object instance.</param>
        public CetiBaseObjectInfo(CetiBaseObject baseObject)
        {
            this.Id = baseObject.Id;
            this.Name = baseObject.Name;
            this.Driver = new CetiDriverInfo(baseObject.Driver);
            this.Runtime = baseObject.Runtime;
            this.InputData = baseObject.InputData;
            this.OutputData = baseObject.OutputData;
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
        public virtual CetiDictionary<string, object> Runtime { get; private set; }

        /// <summary>
        /// Gets the input data.
        /// </summary>
        public virtual CetiInputData InputData { get; private set; }

        /// <summary>
        /// Gets the output data.
        /// </summary>
        public virtual CetiOutputData OutputData { get; private set; }

        #endregion
    }
}
