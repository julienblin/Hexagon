// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITypeFactory.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents a factory that gets instances based
//   on type information only.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using System;

    /// <summary>
    /// Represents a factory that gets instances based
    /// on type information only.
    /// </summary>
    public interface ITypeFactory
    {
        /// <summary>
        /// Gets an instance of type <paramref name="type"/> and
        /// cast to <typeparamref name="T"/>.
        /// All objects created here must be released after usage with <see cref="Release"/>.
        /// </summary>
        /// <param name="type">
        /// The object type.
        /// </param>
        /// <typeparam name="T">
        /// The base type to return.
        /// </typeparam>
        /// <returns>
        /// The instance, or null if it could not be built.
        /// </returns>
        T Get<T>(Type type) where T : class;

        /// <summary>
        /// Releases objects created by <see cref="Get{T}"/>.
        /// </summary>
        /// <param name="obj">
        /// The object to release.
        /// </param>
        void Release(object obj);
    }
}
