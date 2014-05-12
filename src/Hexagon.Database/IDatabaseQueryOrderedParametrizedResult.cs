// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseQueryOrderedParametrizedResult.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents ordered results returned from a <see cref="IDatabaseQueryParametrizedResult{TResult}" />
//   that defers the execution and allow further common parameterization.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database
{
    using System;
    using System.Collections.Generic;

    using Hexagon.Messages;

    /// <summary>
    /// Represents ordered results returned from a <see cref="IDatabaseQueryParametrizedResult{TResult}"/>
    /// that defers the execution and allow further common parameterization.
    /// </summary>
    /// <typeparam name="T">
    /// The real type of the results.
    /// </typeparam>
    public interface IDatabaseQueryOrderedParametrizedResult<T>
    {
        /// <summary>
        /// Performs a subsequent ordering operation in an ascending manner using the <paramref name="selector"/>.
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
        IDatabaseQueryOrderedParametrizedResult<T> ThenBy<TKey>(Func<T, TKey> selector);

        /// <summary>
        /// Performs a subsequent ordering operation in an descending manner using the <paramref name="selector"/>.
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
        IDatabaseQueryOrderedParametrizedResult<T> ThenByDescending<TKey>(Func<T, TKey> selector);

        /// <summary>
        /// Returns a slice of all the results based on
        /// <paramref name="page"/> number and <paramref name="perPage"/> items.
        /// </summary>
        /// <param name="page">
        /// The page number. Defaults to 1.
        /// </param>
        /// <param name="perPage">
        /// The number of items per page. Defaults to 30.
        /// </param>
        /// <returns>
        /// The <see cref="IPaginationResults"/>.
        /// </returns>
        IPaginationResults<T> Paginate(int page = 1, int perPage = 30);

        /// <summary>
        /// Returns a slice of all the results based on <paramref name="paginationParameters"/>.
        /// </summary>
        /// <param name="paginationParameters">
        /// The pagination parameters.
        /// </param>
        /// <returns>
        /// The <see cref="IPaginationResults"/>.
        /// </returns>
        IPaginationResults<T> Paginate(IPaginationParameters paginationParameters);

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
