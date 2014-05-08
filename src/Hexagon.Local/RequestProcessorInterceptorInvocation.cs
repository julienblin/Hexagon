// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestProcessorInterceptorInvocation.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IRequestProcessorInvocation" /> that invokes an <see cref="IRequestProcessorInterceptor" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Local
{
    using Hexagon.Messages;

    /// <summary>
    /// <see cref="IRequestProcessorInvocation"/> that invokes an <see cref="IRequestProcessorInterceptor"/>
    /// </summary>
    internal class RequestProcessorInterceptorInvocation : IRequestProcessorInvocation
    {
        /// <summary>
        /// The interceptor to invoke.
        /// </summary>
        private readonly IRequestProcessorInterceptor interceptor;

        /// <summary>
        /// The next invocation in the chain.
        /// </summary>
        private readonly IRequestProcessorInvocation invocation;

        /// <summary>
        /// The logger.
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestProcessorInterceptorInvocation"/> class.
        /// </summary>
        /// <param name="interceptor">
        /// The interceptor to invoke.
        /// </param>
        /// <param name="invocation">
        /// The next invocation in the chain.
        /// </param>
        public RequestProcessorInterceptorInvocation(IRequestProcessorInterceptor interceptor, IRequestProcessorInvocation invocation)
        {
            Guard.AgainstNull(() => interceptor, interceptor);
            Guard.AgainstNull(() => invocation, invocation);

            this.interceptor = interceptor;
            this.invocation = invocation;
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
            this.invocation.Request = this.Request;
            this.invocation.Response = this.Response;

            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug("Invoking interceptor {0} for ({1},{2}).", this.interceptor, this.invocation.Request, this.invocation.Response);
            }

            this.interceptor.Intercept(this.invocation);
            
            this.Request = this.invocation.Request;
            this.Response = this.invocation.Response;
        }
    }
}
