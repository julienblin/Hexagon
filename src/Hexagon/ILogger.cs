// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents a logger.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets a value indicating whether this logger is enabled for the Debug level.
        /// </summary>
        bool IsDebugEnabled
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this logger is enabled for the Info level.
        /// </summary>
        bool IsInfoEnabled
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this logger is enabled for the Warn level.
        /// </summary>
        bool IsWarnEnabled
        {
            get;
        }

        /// <summary>
        /// Logs a Debug message.
        /// Represents usually detailed information on the flow through the system.
        /// Expected to be disabled in production.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void Debug(object message);

        /// <summary>
        /// Logs an Info message.
        /// Represents usually interesting runtime events (startup/shutdown).
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void Info(object message);

        /// <summary>
        /// Logs a Warn message.
        /// Represents usually the use of deprecated APIs, poor use of API, almost errors,
        /// and other runtime situations that are undesirable or unexpected but are recoverable.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void Warn(object message);

        /// <summary>
        /// Logs a Warn message.
        /// Represents usually the use of deprecated APIs, poor use of API, almost errors,
        /// and other runtime situations that are undesirable or unexpected but are recoverable.
        /// </summary>
        /// <param name="ex">
        /// The exception.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        void Warn(Exception ex, object message);

        /// <summary>
        /// Logs an Error message.
        /// Represents usually runtime errors or unexpected conditions, that cannot be 
        /// recovered but do not cause premature termination.
        /// The majority of errors falls into that category.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void Error(object message);

        /// <summary>
        /// Logs an Error message.
        /// Represents usually runtime errors or unexpected conditions, that cannot be 
        /// recovered but do not cause premature termination.
        /// The majority of errors falls into that category.
        /// </summary>
        /// <param name="ex">
        /// The exception.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        void Error(Exception ex, object message);

        /// <summary>
        /// Logs an Fatal message.
        /// Represents usually severe errors that cause premature termination.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void Fatal(object message);

        /// <summary>
        /// Logs an Fatal message.
        /// Represents usually severe errors that cause premature termination.
        /// </summary>
        /// <param name="ex">
        /// The exception.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        void Fatal(Exception ex, object message);
    }

    /// <summary>
    /// Holds extension methods for <see cref="ILogger"/>.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Logs a Debug message with format parameters.
        /// Represents usually detailed information on the flow through the system.
        /// Expected to be disabled in production.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format.
        /// </param>
        public static void Debug(this ILogger logger, string format, params object[] args)
        {
            logger.Debug(SwallowErrorFormat(logger, format, args));
        }

        /// <summary>
        /// Logs an Info message with format parameters.
        /// Represents usually interesting runtime events (startup/shutdown).
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format.
        /// </param>
        public static void Info(this ILogger logger, string format, params object[] args)
        {
            logger.Info(SwallowErrorFormat(logger, format, args));
        }

        /// <summary>
        /// Logs a Warn message with format parameters.
        /// Represents usually the use of deprecated APIs, poor use of API, almost errors,
        /// and other runtime situations that are undesirable or unexpected but are recoverable.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format.
        /// </param>
        public static void Warn(this ILogger logger, string format, params object[] args)
        {
            logger.Warn(SwallowErrorFormat(logger, format, args));
        }

        /// <summary>
        /// Logs a Warn message with format parameters.
        /// Represents usually the use of deprecated APIs, poor use of API, almost errors,
        /// and other runtime situations that are undesirable or unexpected but are recoverable.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="ex">
        /// The exception.
        /// </param>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format.
        /// </param>
        public static void Warn(this ILogger logger, Exception ex, string format, params object[] args)
        {
            logger.Warn(ex, SwallowErrorFormat(logger, format, args));
        }

        /// <summary>
        /// Logs an Error message with format parameters.
        /// Represents usually runtime errors or unexpected conditions, that cannot be 
        /// recovered but do not cause premature termination.
        /// The majority of errors falls into that category.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format.
        /// </param>
        public static void Error(this ILogger logger, string format, params object[] args)
        {
            logger.Error(SwallowErrorFormat(logger, format, args));
        }

        /// <summary>
        /// Logs an Error message with format parameters.
        /// Represents usually runtime errors or unexpected conditions, that cannot be 
        /// recovered but do not cause premature termination.
        /// The majority of errors falls into that category.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="ex">
        /// The exception.
        /// </param>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format.
        /// </param>
        public static void Error(this ILogger logger, Exception ex, string format, params object[] args)
        {
            logger.Error(ex, SwallowErrorFormat(logger, format, args));
        }

        /// <summary>
        /// Logs a Fatal message with format parameters.
        /// Represents usually severe errors that cause premature termination.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format.
        /// </param>
        public static void Fatal(this ILogger logger, string format, params object[] args)
        {
            logger.Fatal(SwallowErrorFormat(logger, format, args));
        }

        /// <summary>
        /// Logs a Fatal message with format parameters.
        /// Represents usually severe errors that cause premature termination.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="ex">
        /// The exception.
        /// </param>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format.
        /// </param>
        public static void Fatal(this ILogger logger, Exception ex, string format, params object[] args)
        {
            logger.Fatal(ex, SwallowErrorFormat(logger, format, args));
        }

        /// <summary>
        /// Executes a <see cref="CultureInfo.InvariantCulture"/> variant of<see cref="string.Format(string,object)"/>
        /// but trap any exception, logs it and return a safe version without formatting, but preserving the original
        /// format and arguments.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format.
        /// </param>
        /// <returns>
        /// The result of <see cref="string.Format(string,object)"/>, or a safe version of it.
        /// </returns>
        private static string SwallowErrorFormat(ILogger logger, string format, params object[] args)
        {
            try
            {
                return string.Format(CultureInfo.InvariantCulture, format, args);
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Error while logging using format string {0} using arguments {1}.", format, string.Join(", ", args));
                return string.Concat(format, "->", string.Join(",", args));
            }
        }
    }
}
