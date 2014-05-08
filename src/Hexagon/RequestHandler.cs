// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Base class for <see cref="IRequestHandler" /> implementations.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using Hexagon.Messages;

    /// <summary>
    /// Base class for <see cref="IRequestHandler{TRequest,TResponse}"/> implementations.
    /// </summary>
    /// <typeparam name="TRequest">
    /// The type of request to handle.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The type of response.
    /// </typeparam>
    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Gets or sets the <see cref="ILogger"/>.
        /// </summary>
        public ILogger Logger
        {
            get
            {
                return this.logger ?? (this.logger = NullLogger.Instance);
            }

            set
            {
                this.logger = value;
            }
        }

        /// <inheritdoc />
        void IRequestHandler.Handle(IRequest request, IResponse response)
        {
            Guard.AgainstNull(() => request, request);
            Guard.AgainstNull(() => response, response);
            Guard.AgainstTypeIncompatibility<TRequest>(() => request, request);
            Guard.AgainstTypeIncompatibility<TResponse>(() => response, response);

            this.Handle((TRequest)request, (TResponse)response);
        }

        /// <summary>
        /// Actual handling of <paramref name="request"/>.
        /// </summary>
        /// <param name="request">
        /// The request to handle.
        /// </param>
        /// <param name="response">
        /// The response to fill.
        /// </param>
        protected abstract void Handle(TRequest request, TResponse response);
    }
}
