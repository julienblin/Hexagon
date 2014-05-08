// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestHandlerInvocation.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IRequestProcessorInvocation" /> that invokes the appropriate
//   <see cref="IRequestHandler{TRequest,TResponse}" /> using <see cref="ITypeFactory" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Local
{
    using Hexagon.Messages;

    /// <summary>
    /// <see cref="IRequestProcessorInvocation"/> that invokes the appropriate
    /// <see cref="IRequestHandler{TRequest,TResponse}"/> using <see cref="ITypeFactory"/>.
    /// </summary>
    internal class RequestHandlerInvocation : IRequestProcessorInvocation
    {
        /// <summary>
        /// The type factory.
        /// </summary>
        private readonly ITypeFactory typeFactory;

        /// <summary>
        /// The logger.
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHandlerInvocation"/> class.
        /// </summary>
        /// <param name="typeFactory">
        /// The type factory used to create <see cref="IRequestHandler{TRequest,TResponse}"/>.
        /// </param>
        public RequestHandlerInvocation(ITypeFactory typeFactory)
        {
            Guard.AgainstNull(() => typeFactory, typeFactory);
            this.typeFactory = typeFactory;
        }

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
        public IRequest Request { get; set; }

        /// <inheritdoc />
        public IResponse Response { get; set; }

        /// <inheritdoc />
        public void Proceed()
        {
            if (this.Request == null)
            {
                throw new HexagonException("Internal error: the request should not be null.");
            }

            if (this.Response == null)
            {
                throw new HexagonException("Internal error: the response should not be null.");
            }

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(
                this.Request.GetType(),
                this.Response.GetType());
            var handler = this.typeFactory.Get<IRequestHandler>(handlerType);

            if (handler == null)
            {
                throw new HexagonException(string.Format("Unable to find an appropriate handler for {0} using type definition {1}.", this.Request, handlerType));
            }

            try
            {
                if (this.logger.IsDebugEnabled)
                {
                    this.logger.Debug("Handling {0} with {1}.", this.Request, handler);
                }

                handler.Handle(this.Request, this.Response);

                if (this.logger.IsDebugEnabled)
                {
                    this.logger.Debug("{0} handled - response is {1}.", this.Request, this.Response);
                }
            }
            finally
            {
                this.typeFactory.Release(handler);
            }
        }
    }
}
