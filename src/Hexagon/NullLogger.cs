// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullLogger.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="ILogger" /> implementation that does nothing.
//   Implemented as a singleton.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using System;

    /// <summary>
    /// <see cref="ILogger"/> implementation that does nothing.
    /// Implemented as a singleton.
    /// </summary>
    public sealed class NullLogger : ILogger
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static readonly NullLogger SingletonInstance = new NullLogger();

        /// <summary>
        /// Prevents a default instance of the <see cref="NullLogger"/> class from being created.
        /// </summary>
        private NullLogger()
        {
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static ILogger Instance
        {
            get
            {
                return SingletonInstance;
            }
        }

        /// <inheritdoc />
        public bool IsDebugEnabled
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool IsInfoEnabled
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool IsWarnEnabled
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc />
        public void Debug(object message)
        {
        }

        /// <inheritdoc />
        public void Info(object message)
        {
        }

        /// <inheritdoc />
        public void Warn(object message)
        {
        }

        /// <inheritdoc />
        public void Warn(Exception ex, object message)
        {
        }

        /// <inheritdoc />
        public void Error(object message)
        {
        }

        /// <inheritdoc />
        public void Error(Exception ex, object message)
        {
        }

        /// <inheritdoc />
        public void Fatal(object message)
        {
        }

        /// <inheritdoc />
        public void Fatal(Exception ex, object message)
        {
        }
    }
}
