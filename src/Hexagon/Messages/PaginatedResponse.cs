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

        /// <inheritdoc />
        public int TotalPages { get; set; }
    }
}
