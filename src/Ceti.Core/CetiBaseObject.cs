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
    public abstract class CetiBaseObject
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class without parameters.
        /// </summary>
        public CetiBaseObject()
        {
            this.Id = Guid.NewGuid();
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
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the base object name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the input data.
        /// </summary>
        public CetiInputData InputData { get; internal set; }

        /// <summary>
        /// Gets the output data.
        /// </summary>
        public CetiOutputData OutputData { get; internal set; }

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
        /// Initializes the base object.
        /// </summary>
        public virtual void Initialize()
        {
            
        }

        /// <summary>
        /// Cleans-up the base object.
        /// </summary>
        public virtual void Cleanup()
        {
            
        }

        /// <summary>
        /// Validates the base object.
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            return true;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the activity instance of the specified type.
        /// </summary>
        /// <returns>The instance of activity runner which is used to run the activity.</returns>
        protected CetiActivityRunner GetActivity<TActivity>() where TActivity : class, ICetiActivityService, new()
        {
            return new CetiActivityRunner(this.Driver, new TActivity());
        }

        #endregion
    }
}
