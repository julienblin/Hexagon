// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents a handler for <see cref="IRequest" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using Hexagon.Messages;

    /// <summary>
    /// Represents a handler for <see cref="IRequest"/>.
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// Handles the <paramref name="request"/> by filling the <paramref name="response"/>.
        /// </summary>
        /// <param name="request">
        /// The request to handle.
        /// </param>
        /// <param name="response">
        /// The response to fill.
        /// </param>
        void Handle(IRequest request, IResponse response);
    }

    /// <summary>
    /// Generic version of <see cref="IRequestHandler"/> used to identify
    /// the type by a factory.
    /// </summary>
    /// <typeparam name="TRequest">
    /// The type of request to handle.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The type of response.
    /// </typeparam>
    public interface IRequestHandler<TRequest, TResponse> : IRequestHandler
        where TRequest : IRequest<TResponse> where TResponse : IResponse
    {
    }
}
