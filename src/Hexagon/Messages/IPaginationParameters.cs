// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPaginationParameters.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents pagination parameters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents pagination parameters.
    /// </summary>
    public interface IPaginationParameters
    {
        /// <summary>
        /// Gets or sets the page number, starts at 1 (and not 0).
        /// </summary>
        [Range(1, Int32.MaxValue)]
        int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the number of entries per page.
        /// </summary>
        [Range(1, Int32.MaxValue)]
        int PerPage { get; set; }
    }
}
