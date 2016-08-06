using Ceti.Core.Runners;
using Ceti.Core.ServiceProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public abstract class CetiTaskRepository
    {
        #region Protected Methods

        /// <summary>
        /// Gets the job instance of the specified type.
        /// </summary>
        /// <returns>The instance of job runner which is used to run the job.</returns>
        protected CetiJobRunner GetJob<TJob>() where TJob : class, ICetiJobService, new()
        {
            return new CetiJobRunner(CetiDriver.LocalDriver, new TJob());
        }

        #endregion
    }

    public abstract class CetiTaskRepository<T> : CetiTaskRepository where T : CetiTaskRepository<T>, new()
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
        static CetiTaskRepository()
        {
            CetiTaskRepository<T>.instances = new Dictionary<Type, T>();
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
    }
}
