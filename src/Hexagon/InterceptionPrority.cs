// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterceptionPrority.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Defines constant values for <see cref="IRequestProcessorInterceptor" /> priorities.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    /// <summary>
    /// Defines constant values for <see cref="IRequestProcessorInterceptor"/> priorities.
    /// </summary>
    /// <see cref="IRequestProcessorInterceptor.InterceptionPriority"/>
    public static class InterceptionPrority
    {
        /// <summary>
        /// High priority - will run first
        /// </summary>
        public const int High = 1;

        /// <summary>
        /// Medium priority
        /// </summary>
        public const int Medium = 5;

        /// <summary>
        /// Low priority - will run last
        /// </summary>
        public const int Low = 10;
    }
}
