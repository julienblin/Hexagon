// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseQueryParametrizedResult.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents results returned from a <see cref="IDatabaseQuery{TResult}" />
//   that defers the execution and allow further common parameterization.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents results returned from a <see cref="IDatabaseQuery{TResult}"/>
    /// that defers the execution and allow further common parameterization.
    /// </summary>
    /// <typeparam name="T">
    /// The real type of the results.
    /// </typeparam>
    public interface IDatabaseQueryParametrizedResult<T>
    {
        /// <summary>
        /// Order results in an ascending manner by the <paramref name="selector"/>.
        /// Segway to pagination.
        /// </summary>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <typeparam name="TKey">
        /// The type of the key to order by.
        /// </typeparam>
        /// <returns>
        /// The <see cref="IDatabaseQueryOrderedParametrizedResult{T}"/>.
        /// </returns>
        IDatabaseQueryOrderedParametrizedResult<T> OrderBy<TKey>(Func<T, TKey> selector);

        /// <summary>
        /// Order results in an descending manner by the <paramref name="selector"/>.
        /// Segway to pagination.
        /// </summary>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <typeparam name="TKey">
        /// The type of the key to order by.
        /// </typeparam>
        /// <returns>
        /// The <see cref="IDatabaseQueryOrderedParametrizedResult{T}"/>.
        /// </returns>
        IDatabaseQueryOrderedParametrizedResult<T> OrderByDescending<TKey>(Func<T, TKey> selector);

        /// <summary>
        /// Returns all the results.
        /// </summary>
        /// <returns>
        /// The results.
        /// </returns>
        IEnumerable<T> List();

        /// <summary>
        /// Counts the number of results.
        /// </summary>
        /// <returns>
        /// The count.
        /// </returns>
        int Count();
    }
}
