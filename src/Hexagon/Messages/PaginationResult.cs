// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaginationResult.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IPaginationResults{T}" /> simple implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="IPaginationResults{T}"/> simple implementation.
    /// </summary>
    /// <typeparam name="T">
    /// The type of items paginated.
    /// </typeparam>
    public class PaginationResult<T> : IPaginationResults<T>
    {
        /// <inheritdoc />
        public int CurrentPage { get; set; }

        /// <inheritdoc />
        public int PerPage { get; set; }

        /// <inheritdoc />
        public int TotalEntries { get; set; }

        /// <inheritdoc />
        public IEnumerable<T> Items { get; set; }
    }
}
