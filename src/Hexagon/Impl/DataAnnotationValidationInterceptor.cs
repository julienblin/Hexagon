// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAnnotationValidationInterceptor.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IRequestProcessorInterceptor" /> that validates the request using
//   data annotations validation and either throws an exception or fill the response
//   if it implements <see cref="IValidationResults" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Impl
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using Hexagon.Messages;

    /// <summary>
    /// <see cref="IRequestProcessorInterceptor"/> that validates the request using
    /// data annotations validation and either throws an exception or fill the response
    /// if it implements <see cref="IValidationResults"/>.
    /// </summary>
    public class DataAnnotationValidationInterceptor : IRequestProcessorInterceptor
    {
        /// <summary>
        /// Gets the interception priority.
        /// Defined as <see cref="DefaultInterceptionPrority.Medium"/>.
        /// </summary>
        public int InterceptionPriority
        {
            get
            {
                return DefaultInterceptionPrority.Medium;
            }
        }

        /// <inheritdoc />
        public void Intercept(IRequestProcessorInvocation invocation)
        {
            var validationResponse = invocation.Response as IValidationResults;
            if (validationResponse != null)
            {
                var validationResults = new Collection<ValidationResult>();
                if (
                    !Validator.TryValidateObject(
                        invocation.Request,
                        new ValidationContext(invocation.Request),
                        validationResults,
                        true))
                {
                    validationResponse.ValidationResults = validationResults;
                    return;
                }
            }
            else
            {
                Validator.ValidateObject(invocation.Request, new ValidationContext(invocation.Request), true);
            }

            invocation.Proceed();
        }
    }
}
