// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestProcessor.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents the main entry point for the processing of <see cref="IRequest" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using System.Threading.Tasks;

    using Hexagon.Messages;

    /// <summary>
    /// Represents the main entry point for the processing of <see cref="IRequest"/>.
    /// </summary>
    public interface IRequestProcessor
    {
        /// <summary>
        /// Processes the <paramref name="request"/> and returns the response.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <typeparam name="TResponse">
        /// The type of response.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TResponse"/>.
        /// </returns>
        TResponse Process<TResponse>(IRequest<TResponse> request) where TResponse : IResponse;

        /// <summary>
        ///  Processes the <paramref name="request"/> asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <typeparam name="TResponse">
        /// The type of response.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task{TResult}"/>.
        /// </returns>
        Task<TResponse> ProcessAsync<TResponse>(IRequest<TResponse> request) where TResponse : IResponse;
    }
}
