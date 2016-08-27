using Ceti.Core.Runners;
using Ceti.Core.ServiceProviders;
using Ceti.Core.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public abstract class CetiWorkflow
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class without parameters.
        /// </summary>
        public CetiWorkflow()
        {
            this.Id = Guid.NewGuid();
            this.Name = this.GetType().FullName;
            this.InputData = null;
            this.OutputData = null;
            this.Driver = null;
            this.Runtime = new CetiDictionary<string, object>();
            this.IsValid = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the base object id.
        /// </summary>
        public virtual Guid Id { get; private set; }

        /// <summary>
        /// Gets the base object name.
        /// </summary>
        public virtual string Name { get; private set; }

        /// <summary>
        /// Gets the input data.
        /// </summary>
        public virtual CetiInputData InputData { get; internal set; }

        /// <summary>
        /// Gets the output data.
        /// </summary>
        public virtual CetiOutputData OutputData { get; internal set; }

        /// <summary>
        /// Gets the driver instance.
        /// </summary>
        public CetiDriver Driver { get; internal set; }

        /// <summary>
        /// Gets the runtime data collection.
        /// </summary>
        public CetiDictionary<string, object> Runtime { get; private set; }

        /// <summary>
        /// Gets the boolean flag for validation status.
        /// </summary>
        public bool IsValid { get; internal set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the workflow object.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// Cleans-up the workflow object.
        /// </summary>
        public virtual void Cleanup()
        {

        }

        /// <summary>
        /// Validates the workflow object.
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            return true;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the channel instance of the specified type.
        /// </summary>
        /// <returns>The instance of channel runner which is used to run the channel.</returns>
        protected CetiChannelRunner GetChannel<TChannel>() where TChannel : class, ICetiChannelService, new()
        {
            return new CetiChannelRunner(this.Driver, new TChannel());
        }

        #endregion
    }
    
    public abstract class CetiWorkflow<TInput, TOutput> : CetiWorkflow where TInput : CetiInputData where TOutput : CetiOutputData
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets input data.
        /// </summary>
        public new TInput InputData
        {
            get
            {
                return (TInput)base.InputData;
            }
            protected set
            {
                base.InputData = value;
            }
        }

        /// <summary>
        /// Gets or sets output data.
        /// </summary>
        public new TOutput OutputData
        {
            get
            {
                return (TOutput)base.OutputData;
            }
            protected set
            {
                base.OutputData = value;
            }
        }

        #endregion
    }
}
