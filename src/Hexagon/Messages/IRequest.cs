// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequest.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents requests that the system can process
//   and returns a response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    using System;

    /// <summary>
    /// Represents requests that the system can process
    /// and returns a response.
    /// </summary>
    public interface IRequest : IMessage
    {
        /// <summary>
        /// Gets the corresponding response <see cref="Type"/>.
        /// Must be a <see cref="IResponse"/>.
        /// </summary>
        Type ResponseType { get; }
    }

    /// <summary>
    /// Represents requests that the system can process
    /// and returns a response.
    /// Generic version of <see cref="IRequest"/>.
    /// </summary>
    /// <typeparam name="TResponse">
    /// The type of the response.
    /// </typeparam>
    public interface IRequest<TResponse> : IRequest
        where TResponse : IResponse
    {
    }
}
