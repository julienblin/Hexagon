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
    using System.Collections.Generic;

    using Hexagon.Messages;

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
        /// Returns all the results.
        /// </summary>
        /// <returns>
        /// The results.
        /// </returns>
        IEnumerable<T> List();

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
        /// Counts the number of results.
        /// </summary>
        /// <returns>
        /// The count.
        /// </returns>
        int Count();
    }
}
