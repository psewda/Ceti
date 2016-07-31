using Ceti.Core.Runners;
using Ceti.Core.ServiceProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public abstract class CetiTaskContainer<T> where T : CetiTaskContainer<T>, new()
    {
        #region Private Fields

        /// <summary>
        /// The lock object for multi-thread synchronization.
        /// </summary>
        private static readonly object syncLock = new object();

        /// <summary>
        /// The list of object instances.
        /// </summary>
        private static Dictionary<Type, T> instances;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class without parameters.
        /// </summary>
        static CetiTaskContainer()
        {
            CetiTaskContainer<T>.instances = new Dictionary<Type, T>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance of the specified generic type.
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (!instances.ContainsKey(typeof(T)))
                    {
                        instances.Add(typeof(T), new T());
                    }
                }
                return instances[typeof(T)];
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the activity instance of the specified type.
        /// </summary>
        /// <returns>The instance of activity runner which is used to run the activity.</returns>
        protected CetiActivityRunner GetActivity<TActivity>() where TActivity : class, ICetiActivityService, new()
        {
            return new CetiActivityRunner(CetiDriver.LocalDriver, new TActivity());
        }

        #endregion
    }
}
