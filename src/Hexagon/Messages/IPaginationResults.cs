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
    /// <summary>
    /// Represents the results of a paginated request.
    /// </summary>
    public interface IPaginationResults : IPaginationParameters
    {
        /// <summary>
        /// Gets or sets the total number of entries without pagination.
        /// </summary>
        int TotalEntries { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        int TotalPages { get; set; }
    }
}
