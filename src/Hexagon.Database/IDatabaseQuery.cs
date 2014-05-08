// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseQuery.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents query parameters for a database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database
{
    /// <summary>
    /// Represents query parameters for a database.
    /// </summary>
    public interface IDatabaseQuery
    {
    }

    /// <summary>
    /// Represents query parameters for a database.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of result returned when executing the query.
    /// </typeparam>
    public interface IDatabaseQuery<TResult> : IDatabaseQuery
    {
    }
}
