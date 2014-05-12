// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDbContextDatabaseCommandHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents a <see cref="IDatabaseCommand" /> handler that
//   uses <see cref="DbContext" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF
{
    using System.Data.Entity;

    /// <summary>
    ///  Represents a <see cref="IDatabaseCommand"/> handler that
    ///  uses <see cref="DbContext"/>.
    /// </summary>
    public interface IDbContextDatabaseCommandHandler
    {
        /// <summary>
        /// Handles the <paramref name="command"/> using the <paramref name="context"/>.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The result, or null is the command return no result.
        /// </returns>
        object Handle(IDatabaseCommand command, DbContext context);
    }

    /// <summary>
    ///  Represents a <see cref="IDatabaseCommand"/> handler that
    ///  uses <see cref="DbContext"/>.
    /// </summary>
    /// <typeparam name="TDatabaseCommand">
    /// The type of database command to handle.
    /// </typeparam>
    public interface IDbContextDatabaseCommandHandler<in TDatabaseCommand> : IDbContextDatabaseCommandHandler
        where TDatabaseCommand : IDatabaseCommand
    {
    }
}
