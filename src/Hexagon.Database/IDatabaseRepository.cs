// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseRepository.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Allows the manipulation of entities stored in a database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database
{
    /// <summary>
    /// Allows the manipulation of entities stored in a database.
    /// </summary>
    public interface IDatabaseRepository
    {
        /// <summary>
        /// Gets the entity by id, or null if not found.
        /// </summary>
        /// <typeparam name="T">
        /// The type of entity to get.
        /// </typeparam>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The entity, or null if not found.
        /// </returns>
        T Get<T>(object id) where T : class;

        /// <summary>
        /// Adds the entity to the repository.
        /// </summary>
        /// <typeparam name="T">
        /// The type of entity to add.
        /// </typeparam>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Add<T>(T entity) where T : class;

        /// <summary>
        /// Removes the entity from the repository.
        /// </summary>
        /// <typeparam name="T">
        /// The type of entity to remove.
        /// </typeparam>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Remove<T>(T entity) where T : class;

        /// <summary>
        /// Queries the repository.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <typeparam name="TResult">
        /// The type of result.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        TResult Execute<TResult>(IDatabaseQuery<TResult> query);

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        void Execute(IDatabaseCommand command);

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <typeparam name="TResult">
        /// The type of result.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        TResult Execute<TResult>(IDatabaseCommand<TResult> command);
    }
}
