// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestProcessorInterceptor.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents an interceptor that can be invoked in a handling chain.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    /// <summary>
    /// Represents an interceptor that can be invoked in a handling chain.
    /// </summary>
    public interface IRequestProcessorInterceptor
    {
        /// <summary>
        /// Gets the priority of the interceptor execution. Please use values from <see cref="DefaultInterceptionPrority"/>.
        /// </summary>
        int InterceptionPriority { get; }

        /// <summary>
        /// Called when the interceptor is asked to intercept.
        /// Must call <see cref="IRequestProcessorInvocation.Proceed()"/> to allow the invocation chain to continue.
        /// </summary>
        /// <param name="invocation">The invocation parameters.</param>
        void Intercept(IRequestProcessorInvocation invocation);
    }
}
