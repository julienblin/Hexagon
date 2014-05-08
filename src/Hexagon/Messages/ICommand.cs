// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents an <see cref="IRequest" /> that incurs side effects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    /// <summary>
    /// Represents an <see cref="IRequest"/> that incurs side effects.
    /// </summary>
    public interface ICommand : IRequest
    {
    }

    /// <summary>
    /// Represents an <see cref="IRequest{TResponse}"/> that incurs side effects.
    /// </summary>
    /// <typeparam name="TResponse">
    /// The type of the response.
    /// </typeparam>
    public interface ICommand<TResponse> : ICommand, IRequest<TResponse>
        where TResponse : IResponse
    {
    }
}
