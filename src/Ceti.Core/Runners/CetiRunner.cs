using Ceti.Core.Exceptions;
using Ceti.Core.ServiceProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Runners
{
    public abstract class CetiRunner
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="driver">The driver instance.</param>
        public CetiRunner(CetiDriver driver)
        {
            this.Driver = driver;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the driver instance.
        /// </summary>
        protected CetiDriver Driver { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the specified workflow/task/activity etc.
        /// </summary>
        /// <param name="inputData">The data for the running object.</param>
        /// <returns>The result of the running object.</returns>
        public abstract CetiOutputData Run(CetiInputData inputData);

        #endregion

        #region Internal Protected Methods

        /// <summary>
        /// Runs 'LoadData' method of all data service providers.
        /// </summary>
        internal protected void ProcessDataService()
        {
            var dataServiceProviders = this.Driver.ServiceProvider.DataService.Instances;
            if (dataServiceProviders != null && dataServiceProviders.Count > 0)
            {
                foreach (var dataServiceProvider in dataServiceProviders)
                {
                    this.Driver.SetGlobalData(dataServiceProvider.LoadData(this.Driver.GlobalData));
                }
            }
        }

        /// <summary>
        /// Runs 'OnExecution' method of all execution service providers.
        /// </summary>
        /// <param name="context">The execution context instance.</param>
        internal protected void ProcessExecutionService(CetiExecutionContext context)
        {
            var executionServiceProviders = this.Driver.ServiceProvider.ExecutionService.Instances;
            if (executionServiceProviders != null && executionServiceProviders.Count > 0)
            {
                foreach (var executionServiceProvider in executionServiceProviders)
                {
                    executionServiceProvider.OnExecution(context);
                }
            }
        }

        /// <summary>
        /// Runs 'OnException' method of all execution service providers.
        /// </summary>
        /// <param name="exception">The exception instance.</param>
        internal protected void ProcessExecutionService(CetiException exception)
        {
            var executionServiceProviders = this.Driver.ServiceProvider.ExecutionService.Instances;
            if (executionServiceProviders != null && executionServiceProviders.Count > 0)
            {
                foreach (var executionServiceProvider in executionServiceProviders)
                {
                    executionServiceProvider.OnException(exception);
                }
            }
        }

        #endregion
    }
}
