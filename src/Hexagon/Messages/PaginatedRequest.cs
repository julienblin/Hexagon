// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaginatedRequest.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IRequest{TResponse}" /> standard abstract implementation that
//   implements <see cref="IPaginationParameters" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    /// <summary>
    /// <see cref="IRequest{TResponse}"/> standard abstract implementation that
    /// implements <see cref="IPaginationParameters"/>
    /// </summary>
    /// <typeparam name="TResponse">
    /// The type of the response.
    /// </typeparam>
    public abstract class PaginatedRequest<TResponse> : Request<TResponse>, IPaginationParameters
        where TResponse : IResponse
    {
        /// <summary>
        /// The default <see cref="PerPage"/> value.
        /// </summary>
        public const int DefaultPerPageValue = 30;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedRequest{TResponse}"/> class.
        /// </summary>
        protected PaginatedRequest()
        {
            this.CurrentPage = 1;
            this.PerPage = DefaultPerPageValue;
        }

        /// <inheritdoc />
        public int CurrentPage { get; set; }

        /// <inheritdoc />
        public int PerPage { get; set; }
    }
}
