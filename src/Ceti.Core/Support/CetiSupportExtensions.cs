using Ceti.Core.Exceptions;
using Ceti.Core.Runners;
using Ceti.Core.ServiceProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Support
{
    internal static class CetiSupportExtensions
    {
        #region Public Methods

        /// <summary>
        /// Compares the specified strings and returns the status.  
        /// </summary>
        /// <param name="str1">The 1st string.</param>
        /// <param name="str2">The 2nd string.</param>
        /// <returns>A boolean flag indicating the comparison status.</returns>
        public static bool IsEqual(this string str1, string str2)
        {
            var s1 = string.IsNullOrEmpty(str1) ? string.Empty : str1;
            var s2 = string.IsNullOrEmpty(str2) ? string.Empty : str2;
            return s1.ToUpper() == s2.ToUpper();
        }

        /// <summary>
        /// Get the entry point agent from workflow.
        /// </summary>
        /// <param name="workflow">The workflow having agents.</param>
        /// <returns>The entry point agent delegate.</returns>
        public static Func<CetiTaskRunnerInfo, CetiAgentSelector> GetEntryPointAgent(this CetiWorkflow workflow)
        {
            return getEntryPointAgent<CetiTaskRunnerInfo>(workflow);
        }

        /// <summary>
        /// Creates interception service queue from the specified service list.
        /// </summary>
        /// <param name="services">The interception service list.</param>
        /// <param name="task">The task method instance.</param>
        /// <param name="inputData">The input data for interception.</param>
        /// <returns>The interception service queue.</returns>
        public static Queue<CetiInterceptionService> CreateServiceQueue(this List<ICetiInterceptionService> services, MethodInfo task, CetiInputData inputData)
        {
            // Create interception service queue
            var queue = new Queue<CetiInterceptionService>();

            // Add service instances in the queue
            if (services != null)
            {
                foreach (var service in services)
                {
                    if (service.InterceptionType == CetiInterceptionType.Task)
                    {
                        if (service.IsRequired(new CetiInterceptionContext(task, inputData)))
                        {
                            queue.Enqueue((CetiInterceptionService)service);
                        }
                    }
                }
            }

            // Return the queue
            return queue;
        }

        /// <summary>
        /// Creates interception service queue from the specified service list.
        /// </summary>
        /// <param name="services">The interception service list.</param>
        /// <param name="activity">The activity type instance.</param>
        /// <param name="inputData">The input data for interception.</param>
        /// <returns>The interception service queue.</returns>
        public static Queue<CetiInterceptionService> CreateServiceQueue(this List<ICetiInterceptionService> services, Type activity, CetiInputData inputData)
        {
            // Create interception service queue
            var queue = new Queue<CetiInterceptionService>();

            // Add service instances in the queue
            if (services != null)
            {
                foreach (var service in services)
                {
                    if (service.InterceptionType == CetiInterceptionType.Activity)
                    {
                        if (service.IsRequired(new CetiInterceptionContext(activity, inputData)))
                        {
                            queue.Enqueue((CetiInterceptionService)service);
                        }
                    }
                }
            }

            // Return the queue
            return queue;
        }

        /// <summary>
        /// Gets next interception service from the queue
        /// </summary>
        /// <param name="interceptionServiceQueue">The queue having interception service instances.</param>
        /// <returns>The interception service instance.</returns>
        public static CetiInterceptionService GetNextInterceptionService(this Queue<CetiInterceptionService> interceptionServiceQueue)
        {
            if (interceptionServiceQueue != null && interceptionServiceQueue.Count > 0)
            {
                return interceptionServiceQueue.Dequeue();
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the entry point agent from the specified base object.
        /// </summary>
        /// <param name="baseObject">The base object having agents.</param>
        /// <returns>The entry point agent delegate.</returns>
        private static Func<T, CetiAgentSelector> getEntryPointAgent<T>(CetiBaseObject baseObject)
        {
            // Check the method is marked with entry point attribute
            Func<MethodInfo, bool> isEntryPoint = (mi) =>
            {
                return mi.GetCustomAttribute(typeof(CetiEntryPointAttribute)) != null ? true : false;
            };

            // Check the method has valid signature
            Func<MethodInfo, bool> isSignValid = (mi) =>
            {
                if (mi.GetParameters().Length == 1)
                {
                    if (mi.GetParameters()[0].ParameterType == typeof(T))
                    {
                        if (mi.ReturnType == typeof(CetiAgentSelector))
                        {
                            return true;
                        }
                    }
                }
                return false;
            };

            // Gets all entry point methods
            var entryPointMethods = baseObject.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(mi => isEntryPoint(mi))
                .Where(mi => isSignValid(mi))
                .ToList();

            // Check there is only single entry point method
            if (entryPointMethods.Count == 1)
            {
                // Create agent delegate from the method
                var agentDelegate = entryPointMethods[0].CreateDelegate(typeof(Func<T, CetiAgentSelector>), baseObject);
                return (Func<T, CetiAgentSelector>)agentDelegate; 
            }
            else
            {
                // Build exception message based on entry point method count
                var message = string.Empty;
                if (entryPointMethods.Count == 0)
                {
                    message = "The entrypoint agent not found.";
                }
                else
                {
                    message = "There are multiple entrypoint agents.";
                }

                // Throw exception if zero or multiple entry point methods
                throw new CetiException(message); // TODO :: throw more specific exception
            }
        }

        #endregion
    }
}
