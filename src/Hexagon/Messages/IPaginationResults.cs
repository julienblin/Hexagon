// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPaginationResults.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents the results of a paginated request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the results of a paginated request.
    /// </summary>
    public interface IPaginationResults : IPaginationParameters
    {
        /// <summary>
        /// Gets or sets the total number of entries without pagination.
        /// </summary>
        int TotalEntries { get; set; }
    }

    /// <summary>
    /// Represents the results of a paginated request with the item list.
    /// </summary>
    /// <typeparam name="T">
    /// The type of items paginated.
    /// </typeparam>
    public interface IPaginationResults<T> : IPaginationResults
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        IEnumerable<T> Items { get; set; }
    }

    /// <summary>
    /// Holds extension methods for <see cref="IPaginationResults"/>.
    /// </summary>
    public static class PaginationResultsExtensions
    {
        /// <summary>
        /// Gets the estimated total number of pages.
        /// </summary>
        /// <param name="paginationResults">
        /// The pagination results.
        /// </param>
        /// <returns>
        /// The estimated number of pages.
        /// </returns>
        public static int PageCount(this IPaginationResults paginationResults)
        {
            if (paginationResults.PerPage == 0)
            {
                return 0;
            }

            return (paginationResults.TotalEntries / paginationResults.PerPage) + (paginationResults.TotalEntries % paginationResults.PerPage == 0 ? 0 : 1);
        }

        /// <summary>
        /// Gets an indication whether there is a previous page.
        /// </summary>
        /// <param name="paginationResults">
        /// The pagination results.
        /// </param>
        /// <returns>
        /// true if there is a previous page, false otherwise.
        /// </returns>
        public static bool HasPreviousPage(this IPaginationResults paginationResults)
        {
            return paginationResults.CurrentPage > 1;
        }

        /// <summary>
        /// Gets an indication whether there is a next page.
        /// </summary>
        /// <param name="paginationResults">
        /// The pagination results.
        /// </param>
        /// <returns>
        /// true if there is a next page, false otherwise.
        /// </returns>
        public static bool HasNextPage(this IPaginationResults paginationResults)
        {
            return paginationResults.CurrentPage < paginationResults.PageCount();
        }

        /// <summary>
        /// Gets an indication whether it is the first page.
        /// </summary>
        /// <param name="paginationResults">
        /// The pagination results.
        /// </param>
        /// <returns>
        /// true if it is the first page, false otherwise.
        /// </returns>
        public static bool IsFirstPage(this IPaginationResults paginationResults)
        {
            return paginationResults.CurrentPage <= 1;
        }

        /// <summary>
        /// Gets an indication whether it is the last page.
        /// </summary>
        /// <param name="paginationResults">
        /// The pagination results.
        /// </param>
        /// <returns>
        /// true if it is the last page, false otherwise.
        /// </returns>
        public static bool IsLastPage(this IPaginationResults paginationResults)
        {
            return paginationResults.CurrentPage >= paginationResults.PageCount();
        }
    }
}
