// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalRequestProcessor.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IRequestProcessor" /> that executes <see cref="IRequestHandler" /> locally.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Local
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using Hexagon.Messages;

    /// <summary>
    /// <see cref="IRequestProcessor"/> that executes <see cref="IRequestHandler"/> locally.
    /// </summary>
    public class LocalRequestProcessor : IRequestProcessor
    {
        /// <summary>
        /// The type factory.
        /// </summary>
        private readonly ITypeFactory factory;

        /// <summary>
        /// The interceptors to apply.
        /// </summary>
        private readonly IEnumerable<IRequestProcessorInterceptor> interceptors;

        /// <summary>
        /// The logger.
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalRequestProcessor"/> class.
        /// </summary>
        /// <param name="factory">
        /// The <see cref="ITypeFactory"/> used to instantiate <see cref="IRequestHandler{TRequest,TResponse}"/>.
        /// </param>
        /// <param name="interceptors">
        /// The interceptors to apply.
        /// A local copy of the enumeration will be kept.
        /// </param>
        public LocalRequestProcessor(ITypeFactory factory, IEnumerable<IRequestProcessorInterceptor> interceptors)
        {
            Guard.AgainstNull(() => factory, factory);

            this.factory = factory;
            this.interceptors = interceptors == null ? Enumerable.Empty<IRequestProcessorInterceptor>() : interceptors.OrderByDescending(x => x.InterceptionPriority).ToArray();
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
        public TResponse Process<TResponse>(IRequest<TResponse> request) where TResponse : class, IResponse
        {
            Guard.AgainstNull(() => request, request);
            try
            {
                return this.ExecuteProcess(request);
            }
            catch (Exception ex)
            {
                var message = string.Format("Error while processing {0}.", request);
                this.Logger.Error(ex, message);
                throw new HexagonException(message, ex);
            }
        }

        /// <inheritdoc />
        public Task<TResponse> ProcessAsync<TResponse>(IRequest<TResponse> request) where TResponse : class, IResponse
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Builds the invocation chain, creates the initial response object
        /// and invokes the top-level <see cref="IRequestProcessorInvocation"/>.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <typeparam name="TResponse">
        /// The type of the response.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TResponse"/> returned by the top-level invocation.
        /// </returns>
        private TResponse ExecuteProcess<TResponse>(IRequest<TResponse> request) where TResponse : class, IResponse
        {
            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug("Processing {0}.", request);
            }

            var topInvocation = this.GetInvocationChain();
            topInvocation.Request = request;
            topInvocation.Response = this.CreateResponse(request);
            var stopwatch = Stopwatch.StartNew();
            topInvocation.Proceed();
            stopwatch.Stop();

            var response = topInvocation.Response as TResponse;

            if (response == null)
            {
                var message =
                    string.Format("Processing error: invalid response type or null value for response {0}.", topInvocation.Response);
                this.Logger.Error(message);
                throw new HexagonException(message);
            }

            response.Context.Headers[InternalHeaderKeys.LocalProcessingTime] = stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug("{0} processed in {1} ms. Returning {2}.", request, stopwatch.ElapsedMilliseconds, response);
            }

            return response;
        }

        /// <summary>
        /// Gets the invocation chain from the list of interceptors,
        /// including a <see cref="RequestHandlerInvocation"/> at the bottom.
        /// </summary>
        /// <returns>
        /// The top-level <see cref="IRequestProcessorInvocation"/>.
        /// </returns>
        private IRequestProcessorInvocation GetInvocationChain()
        {
            IRequestProcessorInvocation currentInvocation = new RequestHandlerInvocation(this.factory) { Logger = this.Logger };
            return this.interceptors.Aggregate(currentInvocation, (current, interceptor) => new RequestProcessorInterceptorInvocation(interceptor, current) { Logger = this.Logger });
        }

        /// <summary>
        /// Creates the response and initialize the context.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <typeparam name="TResponse">
        /// The type of the response.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TResponse"/>.
        /// </returns>
        /// <exception cref="HexagonException">
        /// If the response could not be created.
        /// </exception>
        private TResponse CreateResponse<TResponse>(IRequest<TResponse> request) where TResponse : IResponse
        {
            try
            {
                var response = Activator.CreateInstance<TResponse>();
                request.Context.InitializeFrom(request.Context);
                return response;
            }
            catch (Exception ex)
            {
                var message = string.Format(
                    "There has been an error while creating response of type {0}. Maybe it's missing a default constructor?",
                    typeof(TResponse));
                this.Logger.Error(ex, message);
                throw new HexagonException(message, ex);
            }
        }
    }
}
