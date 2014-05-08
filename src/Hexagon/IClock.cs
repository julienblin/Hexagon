// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClock.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents a clock, which can give the instant.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using System;

    /// <summary>
    /// Represents a clock, which can give the instant.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Gets the current instant.
        /// </summary>
        DateTimeOffset Now { get; }
    }
}
