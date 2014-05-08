// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationResults.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represent the results of a validation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represent the results of a validation.
    /// </summary>
    public interface IValidationResults
    {
        /// <summary>
        /// Gets or sets the validation results.
        /// </summary>
        IEnumerable<ValidationResult> ValidationResults { get; set; }

        /// <summary>
        /// Gets a value indicating whether the validation failed.
        /// </summary>
        bool IsValid { get; }
    }
}
