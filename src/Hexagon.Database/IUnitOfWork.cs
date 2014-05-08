// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents a unit of work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database
{
    using System;

    /// <summary>
    /// Represents a unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether it is active.
        /// (i.e. has been started and not yet disposed or committed.)
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Starts the unit of work.
        /// </summary>
        void Start();

        /// <summary>
        /// Commits the changes.
        /// </summary>
        /// <remarks>
        /// A <see cref="IUnitOfWork"/> cannot be committed if it
        /// hasn't been started first.
        /// </remarks>
        void Commit();
    }
}
