using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Support
{
    public class StandardOutputData<T> : CetiOutputData
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Value { get; set; }

        #endregion
    }

    public class StandardOutputData<T1, T2> : CetiOutputData
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T1 Value1 { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T2 Value2 { get; set; }

        #endregion
    }

    public class StandardOutputData<T1, T2, T3> : StandardOutputData<T1, T2>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T3 Value3 { get; set; }

        #endregion
    }

    public class StandardOutputData<T1, T2, T3, T4> : StandardOutputData<T1, T2, T3>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T4 Value4 { get; set; }

        #endregion
    }
}
