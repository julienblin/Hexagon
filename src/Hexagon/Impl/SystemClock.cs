// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemClock.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IClock" /> implementation bound to the machine clock.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Impl
{
    using System;

    /// <summary>
    /// <see cref="IClock"/> implementation bound to the machine clock.
    /// </summary>
    public sealed class SystemClock : IClock
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static readonly SystemClock SingletonInstance = new SystemClock();

        /// <summary>
        /// Prevents a default instance of the <see cref="SystemClock"/> class from being created.
        /// </summary>
        private SystemClock()
        {
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static IClock Instance
        {
            get
            {
                return SingletonInstance;
            }
        }

        /// <inheritdoc />
        public DateTimeOffset Now
        {
            get
            {
                return DateTimeOffset.Now;
            }
        }
    }
}
