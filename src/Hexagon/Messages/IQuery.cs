// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQuery.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents a side-effect free <see cref="IRequest" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    /// <summary>
    /// Represents a side-effect free <see cref="IRequest"/>.
    /// </summary>
    public interface IQuery : IRequest
    {
    }

    /// <summary>
    /// Represents a side-effect free <see cref="IRequest{TResponse}"/>.
    /// </summary>
    /// <typeparam name="TResponse">
    /// The type of the response.
    /// </typeparam>
    public interface IQuery<TResponse> : IQuery, IRequest<TResponse>
        where TResponse : IResponse
    {
    }
}
