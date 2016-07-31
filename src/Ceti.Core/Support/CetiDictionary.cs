using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Support
{
    public class CetiDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        #region Private Fields

        /// <summary>
        /// The lock object for multi-thread synchronization.
        /// </summary>
        private static readonly object syncLock = new object();

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the specified value in the dictionary.
        /// </summary>
        /// <param name="key">The key assocciated with the value.</param>
        /// <param name="value">The value assocciated with the key.</param>
        public void SetValue(TKey key, TValue value)
        {
            lock (syncLock)
            {
                if (this.ContainsKey(key))
                {
                    this[key] = value;
                }
                else
                {
                    this.Add(key, value);
                }
            }
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="defaultValue">The value to be returned if the specified key is not availabe in the dictionary.</param>
        /// <returns>The value associated with the specified key.</returns>
        public TValue GetValue(TKey key, TValue defaultValue)
        {
            return this.ContainsKey(key) ? this[key] : defaultValue;
        }

        #endregion
    }
}
