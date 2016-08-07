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
        /// Gets next item from the queue.
        /// </summary>
        /// <param name="queue">The queue having items.</param>
        /// <returns>The next item from the queue.</returns>
        public static T Dequeue<T>(this Queue<T> queue)
        {
            if (queue != null && queue.Count > 0)
            {
                return queue.Dequeue();
            }
            else
            {
                return default(T);
            }
        }

        #endregion
    }
}
