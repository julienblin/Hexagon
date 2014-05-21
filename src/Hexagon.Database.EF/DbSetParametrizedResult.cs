// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbSetParametrizedResult.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IDatabaseQueryParametrizedResult{T}" /> implementation based on
//   a <see cref="DbSet{TEntity}" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// <see cref="IDatabaseQueryParametrizedResult{T}"/> implementation based on
    /// a <see cref="DbSet{TEntity}"/>.
    /// </summary>
    /// <typeparam name="T">
    ///  The real type of the results.
    /// </typeparam>
    public class DbSetParametrizedResult<T> : IDatabaseQueryParametrizedResult<T>
        where T : class
    {
        /// <summary>
        /// The set.
        /// </summary>
        private readonly DbSet<T> set;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSetParametrizedResult{T}"/> class.
        /// </summary>
        /// <param name="set">
        /// The set to act upon.
        /// </param>
        public DbSetParametrizedResult(DbSet<T> set)
        {
            Guard.AgainstNull(() => set, set);
            this.set = set;
        }

        /// <inheritdoc />
        public IDatabaseQueryOrderedParametrizedResult<T> OrderBy<TKey>(Func<T, TKey> selector)
        {
            return new DbSetOrderedParametrizedResult<T>(this.set.OrderBy(selector));
        }

        /// <inheritdoc />
        public IDatabaseQueryOrderedParametrizedResult<T> OrderByDescending<TKey>(Func<T, TKey> selector)
        {
            return new DbSetOrderedParametrizedResult<T>(this.set.OrderByDescending(selector));
        }

        /// <inheritdoc />
        public T FirstOrDefault()
        {
            return this.set.FirstOrDefault();
        }

        /// <inheritdoc />
        public IEnumerable<T> List()
        {
            return this.set.ToList();
        }

        /// <inheritdoc />
        public int Count()
        {
            return this.set.Count();
        }
    }
}
