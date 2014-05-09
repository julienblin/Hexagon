// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IParametrizedDatabaseQuery.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents a <see cref="IDatabaseQuery{TResult}" /> that defers execution and
//   returns <see cref="IParametrizedDatabaseQuery{T}" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database
{
    /// <summary>
    /// Represents a <see cref="IDatabaseQuery{TResult}"/> that defers execution and
    /// returns <see cref="IDatabaseQueryParametrizedResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of result returned when executing the query.
    /// </typeparam>
    public interface IParametrizedDatabaseQuery<T> : IDatabaseQuery<IDatabaseQueryParametrizedResult<T>>
    {
    }
}
