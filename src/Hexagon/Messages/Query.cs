// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Query.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IQuery{TResponse}" /> standard abstract implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    /// <summary>
    /// <see cref="IQuery{TResponse}"/> standard abstract implementation.
    /// </summary>
    /// <typeparam name="TResponse">
    /// The type of the response.
    /// </typeparam>
    public abstract class Query<TResponse> : Request<TResponse>, IQuery<TResponse>
        where TResponse : IResponse
    {
    }
}
