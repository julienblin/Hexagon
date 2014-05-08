// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestConsoleLogger.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="ILogger" /> implementation to be used in unit tests
//   that outputs the messages and exception to the console.
//   Very simple implementation, do not use in production.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// <see cref="ILogger"/> implementation to be used in unit tests
    /// that outputs the messages and exception to the console.
    /// Very simple implementation, do not use in production.
    /// </summary>
    public sealed class TestConsoleLogger : ILogger
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static readonly TestConsoleLogger SingletonInstance = new TestConsoleLogger();

        /// <summary>
        /// Prevents a default instance of the <see cref="TestConsoleLogger"/> class from being created.
        /// </summary>
        private TestConsoleLogger()
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
                return true;
            }
        }

        /// <inheritdoc />
        public bool IsInfoEnabled
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        public bool IsWarnEnabled
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        [SuppressMessage(
        "Microsoft.Globalization",
        "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Console.WriteLine(System.String,System.Object)",
        Justification = "Class used for unit tests.")]
        public void Debug(object message)
        {
            Console.WriteLine("Debug {0}", message);
        }

        /// <inheritdoc />
        [SuppressMessage(
        "Microsoft.Globalization",
        "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Console.WriteLine(System.String,System.Object)",
        Justification = "Class used for unit tests.")]
        public void Info(object message)
        {
            Console.WriteLine("Info  {0}", message);
        }

        /// <inheritdoc />
        [SuppressMessage(
        "Microsoft.Globalization",
        "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Console.WriteLine(System.String,System.Object)",
        Justification = "Class used for unit tests.")]
        public void Warn(object message)
        {
            Console.WriteLine("Warn  {0}", message);
        }

        /// <inheritdoc />
        [SuppressMessage(
        "Microsoft.Globalization",
        "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Console.WriteLine(System.String,System.Object)",
        Justification = "Class used for unit tests.")]
        public void Warn(Exception ex, object message)
        {
            Console.WriteLine("Warn  {0}", message);
            Console.WriteLine(ex);
        }

        /// <inheritdoc />
        [SuppressMessage(
        "Microsoft.Globalization",
        "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Console.WriteLine(System.String,System.Object)",
        Justification = "Class used for unit tests.")]
        public void Error(object message)
        {
            Console.WriteLine("Error  {0}", message);
        }

        /// <inheritdoc />
        [SuppressMessage(
        "Microsoft.Globalization",
        "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Console.WriteLine(System.String,System.Object)",
        Justification = "Class used for unit tests.")]
        public void Error(Exception ex, object message)
        {
            Console.WriteLine("Error  {0}", message);
            Console.WriteLine(ex);
        }

        /// <inheritdoc />
        [SuppressMessage(
        "Microsoft.Globalization",
        "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Console.WriteLine(System.String,System.Object)",
        Justification = "Class used for unit tests.")]
        public void Fatal(object message)
        {
            Console.WriteLine("Fatal  {0}", message);
        }

        /// <inheritdoc />
        [SuppressMessage(
        "Microsoft.Globalization",
        "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Console.WriteLine(System.String,System.Object)",
        Justification = "Class used for unit tests.")]
        public void Fatal(Exception ex, object message)
        {
            Console.WriteLine("Fatal  {0}", message);
            Console.WriteLine(ex);
        }
    }
}
