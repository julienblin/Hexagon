// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbContextDatabaseCommandHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Base class for <see cref="IDbContextDatabaseCommandHandler{TDatabaseCommand}" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Base class for <see cref="IDbContextDatabaseCommandHandler{TDatabaseCommand}"/>.
    /// </summary>
    /// <typeparam name="TDatabaseCommand">
    /// The type of database command to handle.
    /// </typeparam>
    public abstract class DbContextDatabaseCommandHandler<TDatabaseCommand> : IDbContextDatabaseCommandHandler<TDatabaseCommand>
        where TDatabaseCommand : IDatabaseCommand
    {
        /// <inheritdoc />
        object IDbContextDatabaseCommandHandler.Handle(IDatabaseCommand command, DbContext context)
        {
            Guard.AgainstNull(() => command, command);
            Guard.AgainstNull(() => context, context);
            Guard.AgainstTypeIncompatibility<TDatabaseCommand>(() => command, command);

            this.Handle((TDatabaseCommand)command, context);
            return null;
        }

        /// <summary>
        /// Handles the <paramref name="command"/> using the <paramref name="context"/>.
        /// </summary>
        /// <param name="command">
        /// The command to handle.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected abstract void Handle(TDatabaseCommand command, DbContext context);
    }

    /// <summary>
    /// Base class for <see cref="IDbContextDatabaseCommandHandler{TDatabaseCommand}"/> for
    /// commands that return results.
    /// </summary>
    /// <typeparam name="TDatabaseCommand">
    /// The type of database command to handle.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of result.
    /// </typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "OK here because it is a generic variant.")]
    public abstract class DbContextDatabaseCommandHandler<TDatabaseCommand, TResult> : IDbContextDatabaseCommandHandler<TDatabaseCommand>
        where TDatabaseCommand : IDatabaseCommand<TResult>
    {
        /// <inheritdoc />
        object IDbContextDatabaseCommandHandler.Handle(IDatabaseCommand command, DbContext context)
        {
            Guard.AgainstNull(() => command, command);
            Guard.AgainstNull(() => context, context);
            Guard.AgainstTypeIncompatibility<TDatabaseCommand>(() => command, command);

            return this.Handle((TDatabaseCommand)command, context);
        }

        /// <summary>
        /// Handles the <paramref name="command"/> using the <paramref name="context"/>.
        /// </summary>
        /// <param name="command">
        /// The command to handle.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        protected abstract TResult Handle(TDatabaseCommand command, DbContext context);
    }
}
