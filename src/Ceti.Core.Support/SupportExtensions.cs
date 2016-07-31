using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Support
{
    public static class SupportExtensions
    {
        #region Public Properties

        /// <summary>
        /// Converts the input data as specified type.
        /// </summary>
        /// <param name="inputData">The input data instance.</param>
        /// <returns>The converted input data of specified type.</returns>
        public static T As<T>(this CetiInputData inputData) where T : CetiInputData
        {
            return (T)inputData;
        }

        /// <summary>
        /// Converts the output data as specified type.
        /// </summary>
        /// <param name="outputData">The output data instance.</param>
        /// <returns>The converted output data of specified type.</returns>
        public static T As<T>(this CetiOutputData outputData) where T : CetiOutputData
        {
            return (T)outputData;
        }

        /// <summary>
        /// Tries to convert the input data as specified type.
        /// </summary>
        /// <param name="inputData">The input data instance.</param>
        /// <returns>The result of conversion of specified type.</returns>
        public static TryResult<T> TryAs<T>(this CetiInputData inputData) where T : CetiInputData
        {
            return inputData is T ? new TryResult<T>(true, (T)inputData) : new TryResult<T>(false, null);
        }

        /// <summary>
        /// Tries to convert the output data as specified type.
        /// </summary>
        /// <param name="outputData">The output data instance.</param>
        /// <returns>The result of conversion of specified type.</returns>
        public static TryResult<T> TryAs<T>(this CetiOutputData outputData) where T : CetiOutputData
        {
            return outputData is T ? new TryResult<T>(true, (T)outputData) : new TryResult<T>(false, null);
        }

        #endregion
    }
}
