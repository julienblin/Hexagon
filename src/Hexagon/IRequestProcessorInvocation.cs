// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestProcessorInvocation.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents an invocation for handling a request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using Hexagon.Messages;

    /// <summary>
    /// Represents an invocation for handling a request.
    /// </summary>
    public interface IRequestProcessorInvocation
    {
        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        IRequest Request { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        IResponse Response { get; set; }

        /// <summary>
        /// Proceed with the next invocation.
        /// </summary>
        void Proceed();
    }
}