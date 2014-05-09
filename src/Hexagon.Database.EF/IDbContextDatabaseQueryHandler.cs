// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDbContextDatabaseQueryHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents a <see cref="IDatabaseQuery{TResult}"/> handler that
//   uses <see cref="DbContext"/>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF
{
    using System.Data.Entity;

    /// <summary>
    ///  Represents a <see cref="IDatabaseQuery{TResult}"/> handler that
    ///  uses <see cref="DbContext"/>.
    /// </summary>
    public interface IDbContextDatabaseQueryHandler
    {
        /// <summary>
        /// Handles the <paramref name="query"/> using the <paramref name="context"/>
        /// and returns the result.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The result.
        /// </returns>
        object Handle(IDatabaseQuery query, DbContext context);
    }

    /// <summary>
    /// Represents a <see cref="IDatabaseQuery{TResult}"/> handler that
    /// uses <see cref="DbContext"/>.
    /// </summary>
    /// <typeparam name="TDatabaseQuery">
    /// The type of database query to handle.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of result returned when executing the query.
    /// </typeparam>
    public interface IDbContextDatabaseQueryHandler<in TDatabaseQuery, out TResult> : IDbContextDatabaseQueryHandler
        where TDatabaseQuery : IDatabaseQuery<TResult>
    {
    }
}
