using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    public class CetiAgentInfo
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with specified parameters.
        /// </summary>
        /// <param name="type">The agent type.</param>
        /// <param name="method">The  agent method.</param>
        /// <param name="isEntryPoint">The boolean flag for entry point agent.</param>
        public CetiAgentInfo(CetiAgentType type, MethodInfo method, bool isEntryPoint = false)
        {
            this.Type = type;
            this.Method = method;
            this.IsEntryPoint = isEntryPoint;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the agent type.
        /// </summary>
        public CetiAgentType Type { get; private set; }

        /// <summary>
        /// Gets the agent method.
        /// </summary>
        public MethodInfo Method { get; private set; }

        /// <summary>
        /// Gets the boolean flag for entry point agent.
        /// </summary>
        public bool IsEntryPoint { get; private set; }

        #endregion
    }
}
