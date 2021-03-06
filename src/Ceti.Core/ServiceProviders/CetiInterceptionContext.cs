﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.ServiceProviders
{
    public class CetiInterceptionContext
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="task">The task method instance.</param>
        /// <param name="inputData">The input data instance.</param>
        public CetiInterceptionContext(MethodInfo task, CetiInputData inputData)
        {
            this.Task = task;
            this.Channel = null;
            this.InputData = inputData;
        }

        /// <summary>
        /// Initializes the class with the specified parameters.
        /// </summary>
        /// <param name="channel">The channel type instance.</param>
        /// <param name="inputData">The input data instance.</param>
        public CetiInterceptionContext(Type channel, CetiInputData inputData)
        {
            this.Task = null;
            this.Channel = channel;
            this.InputData = inputData;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the task method.
        /// </summary>
        public MethodInfo Task { get; private set; }

        /// <summary>
        /// Gets the channel type.
        /// </summary>
        public Type Channel { get; private set; }

        /// <summary>
        /// Gets the input data.
        /// </summary>
        public CetiInputData InputData { get; private set; }

        #endregion
    }
}
