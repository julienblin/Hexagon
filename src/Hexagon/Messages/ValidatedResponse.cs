// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatedResponse.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IResponse" /> standard abstract implementation that implements
//   <see cref="IValidationResults" /> to include validation results.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// <see cref="IResponse"/> standard abstract implementation that implements
    /// <see cref="IValidationResults"/> to include validation results.
    /// </summary>
    public abstract class ValidatedResponse : Response, IValidationResults
    {
        /// <summary>
        /// The validation results.
        /// </summary>
        private IEnumerable<ValidationResult> validationResults;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> ValidationResults
        {
            get
            {
                return this.validationResults ?? Enumerable.Empty<ValidationResult>();
            }

            set
            {
                this.validationResults = value;
            }
        }

        /// <inheritdoc />
        public bool IsValid
        {
            get
            {
                return !this.ValidationResults.Any();
            }
        }
    }
}
