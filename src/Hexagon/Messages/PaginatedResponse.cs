// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaginatedResponse.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IResponse" /> standard abstract implementation that
//   implements <see cref="IPaginationResults" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// <see cref="IResponse"/> standard abstract implementation that 
    /// implements <see cref="IPaginationResults"/>.
    /// </summary>
    public abstract class PaginatedResponse : Response, IPaginationResults
    {
        /// <inheritdoc />
        public int CurrentPage { get; set; }

        /// <inheritdoc />
        public int PerPage { get; set; }

        /// <inheritdoc />
        public int TotalEntries { get; set; }
    }

    /// <summary>
    /// <see cref="IResponse"/> standard abstract implementation that 
    /// implements <see cref="IPaginationResults{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of items paginated.
    /// </typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "OK here because it is a generic variant.")]
    public abstract class PaginatedResponse<T> : PaginatedResponse, IPaginationResults<T>
    {
        /// <inheritdoc />
        public IEnumerable<T> Items { get; set; }
    }
}
