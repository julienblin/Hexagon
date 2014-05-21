// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbSetOrderedParametrizedResult.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IDatabaseQueryOrderedParametrizedResult{T}" /> implementation that goes along with
//   <see cref="DbSetParametrizedResult{T}" /> for ordering and pagination.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Hexagon.Messages;

    /// <summary>
    /// <see cref="IDatabaseQueryOrderedParametrizedResult{T}"/> implementation that goes along with
    /// <see cref="DbSetParametrizedResult{T}"/> for ordering and pagination.
    /// </summary>
    /// <typeparam name="T">
    /// The real type of the results.
    /// </typeparam>
    internal class DbSetOrderedParametrizedResult<T> : IDatabaseQueryOrderedParametrizedResult<T>
    {
        /// <summary>
        /// The ordered set, result of an order by operation.
        /// </summary>
        private IOrderedEnumerable<T> orderedSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSetOrderedParametrizedResult{T}"/> class.
        /// </summary>
        /// <param name="orderedSet">
        /// The ordered set.
        /// </param>
        public DbSetOrderedParametrizedResult(IOrderedEnumerable<T> orderedSet)
        {
            Guard.AgainstNull(() => orderedSet, orderedSet);
            this.orderedSet = orderedSet;
        }

        /// <inheritdoc />
        public IDatabaseQueryOrderedParametrizedResult<T> ThenBy<TKey>(Func<T, TKey> selector)
        {
            this.orderedSet = this.orderedSet.ThenBy(selector);
            return this;
        }

        /// <inheritdoc />
        public IDatabaseQueryOrderedParametrizedResult<T> ThenByDescending<TKey>(Func<T, TKey> selector)
        {
            this.orderedSet = this.orderedSet.ThenByDescending(selector);
            return this;
        }

        /// <inheritdoc />
        public IPaginationResults<T> Paginate(IPaginationParameters paginationParameters)
        {
            Guard.AgainstNull(() => paginationParameters, paginationParameters);
            return this.Paginate(paginationParameters.CurrentPage, paginationParameters.PerPage);
        }

        /// <inheritdoc />
        public IPaginationResults<T> Paginate(int page = 1, int perPage = 30)
        {
            Guard.AgainstMinimalValue(() => page, page, 1);
            Guard.AgainstMinimalValue(() => page, perPage, 1);

            var count = this.orderedSet.Count();
            return new PaginationResult<T>
            {
                CurrentPage = page,
                PerPage = perPage,
                TotalEntries = count,
                Items = this.orderedSet.Skip((page - 1) * perPage).Take(perPage).ToList()
            };
        }

        /// <inheritdoc />
        public T FirstOrDefault()
        {
            return this.orderedSet.FirstOrDefault();
        }

        /// <inheritdoc />
        public IEnumerable<T> List()
        {
            return this.orderedSet.ToList();
        }

        /// <inheritdoc />
        public int Count()
        {
            return this.orderedSet.Count();
        }
    }
}
