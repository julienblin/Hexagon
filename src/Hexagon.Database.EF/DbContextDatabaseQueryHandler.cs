// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbContextDatabaseQueryHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Base class for <see cref="IDbContextDatabaseQueryHandler{TDatabaseQuery,TResult}" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF
{
    using System.Data.Entity;

    /// <summary>
    /// Base class for <see cref="IDbContextDatabaseQueryHandler{TDatabaseQuery,TResult}"/>.
    /// </summary>
    /// <typeparam name="TDatabaseQuery">
    /// The type of database query to handle.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of result returned when executing the query.
    /// </typeparam>
    public abstract class DbContextDatabaseQueryHandler<TDatabaseQuery, TResult> : IDbContextDatabaseQueryHandler<TDatabaseQuery, TResult>
        where TDatabaseQuery : IDatabaseQuery<TResult>
    {
        /// <inheritdoc />
        object IDbContextDatabaseQueryHandler.Handle(IDatabaseQuery query, DbContext context)
        {
            Guard.AgainstNull(() => query, query);
            Guard.AgainstNull(() => context, context);
            Guard.AgainstTypeIncompatibility<TDatabaseQuery>(() => query, query);
            return this.Handle((TDatabaseQuery)query, context);
        }

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
        /// The <see cref="TResult"/>.
        /// </returns>
        protected abstract TResult Handle(TDatabaseQuery query, DbContext context);
    }
}
