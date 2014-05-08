// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Request.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IRequest{TResponse}" /> standard abstract implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    using System;

    /// <summary>
    /// <see cref="IRequest{TResponse}"/> standard abstract implementation.
    /// </summary>
    /// <typeparam name="TResponse">
    /// The type of the response.
    /// </typeparam>
    public abstract class Request<TResponse> : Message, IRequest<TResponse>
        where TResponse : IResponse
    {
        /// <inheritdoc />
        public virtual Type ResponseType
        {
            get
            {
                return typeof(TResponse);
            }
        }
    }
}
