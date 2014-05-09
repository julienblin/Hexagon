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
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Hexagon.Messages;

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
        public IEnumerable<T> List()
        {
            return this.set.ToList();
        }

        /// <inheritdoc />
        public IPaginationResults<T> Paginate(IPaginationParameters paginationParameters)
        {
            return this.Paginate(paginationParameters.CurrentPage, paginationParameters.PerPage);
        }

        /// <inheritdoc />
        public IPaginationResults<T> Paginate(int page = 1, int perPage = 30)
        {
            var count = this.set.Count();
            return new PaginationResult<T>
                       {
                           CurrentPage = page,
                           PerPage = perPage,
                           TotalEntries = count,
                           Items = this.set.Skip((page - 1) * perPage).Take(perPage)
                       };
        }

        /// <inheritdoc />
        public int Count()
        {
            return this.set.Count();
        }
    }
}
