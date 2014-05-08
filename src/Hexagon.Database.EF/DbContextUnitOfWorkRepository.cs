// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbContextUnitOfWorkRepository.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IUnitOfWork" /> and <see cref="IDatabaseRepository" /> implementation
//   that uses Entity Framework <see cref="DbContext" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF
{
    using System.Data;
    using System.Data.Entity;

    /// <summary>
    /// <see cref="IUnitOfWork"/> and <see cref="IDatabaseRepository"/> implementation
    /// that uses Entity Framework <see cref="DbContext"/>.
    /// </summary>
    public class DbContextUnitOfWorkRepository : DbContext, IUnitOfWork, IDatabaseRepository
    {
        /// <summary>
        /// The transaction isolation level.
        /// </summary>
        private readonly IsolationLevel transactionIsolationLevel = IsolationLevel.ReadCommitted;

        /// <summary>
        /// The logger.
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// The transaction.
        /// </summary>
        private DbContextTransaction transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextUnitOfWorkRepository"/> class.
        /// Important for various EF tooling to work. Should not be used in production.
        /// </summary>
        public DbContextUnitOfWorkRepository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextUnitOfWorkRepository"/> class.
        /// Uses <see cref="IsolationLevel.ReadCommitted"/> for transactions.
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// Either the database name or a connection string.
        /// </param>
        public DbContextUnitOfWorkRepository(string nameOrConnectionString)
            : this(nameOrConnectionString, IsolationLevel.ReadCommitted)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextUnitOfWorkRepository"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// Either the database name or a connection string.
        /// </param>
        /// <param name="transactionIsolationLevel">
        /// The transaction isolation level.
        /// </param>
        public DbContextUnitOfWorkRepository(string nameOrConnectionString, IsolationLevel transactionIsolationLevel)
            : base(nameOrConnectionString)
        {
            Guard.AgainstNullOrEmpty(() => nameOrConnectionString, nameOrConnectionString);
            this.transactionIsolationLevel = transactionIsolationLevel;
        }

        /// <summary>
        /// Gets or sets the <see cref="ILogger"/>.
        /// </summary>
        public ILogger Logger
        {
            get
            {
                return this.logger ?? (this.logger = NullLogger.Instance);
            }

            set
            {
                this.logger = value;
            }
        }

        /// <inheritdoc />
        public virtual bool IsActive
        {
            get
            {
                return this.transaction != null;
            }
        }

        /// <inheritdoc />
        public virtual T Get<T>(object id) where T : class
        {
            this.ThrowIfNotActive("Get");
            return this.Set<T>().Find(id);
        }

        /// <inheritdoc />
        public virtual void Add<T>(T entity) where T : class
        {
            Guard.AgainstNull(() => entity, entity);
            this.ThrowIfNotActive("Add");
            this.Set<T>().Add(entity);
        }

        /// <inheritdoc />
        public virtual void Remove<T>(T entity) where T : class
        {
            Guard.AgainstNull(() => entity, entity);
            this.ThrowIfNotActive("Remove");
            this.Set<T>().Remove(entity);
        }

        /// <inheritdoc />
        public virtual TResult Execute<TResult>(IDatabaseQuery<TResult> query)
        {
            Guard.AgainstNull(() => query, query);
            this.ThrowIfNotActive("Execute query");
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public virtual void Execute(IDatabaseCommand command)
        {
            Guard.AgainstNull(() => command, command);
            this.ThrowIfNotActive("Execute command");
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public virtual TResult Execute<TResult>(IDatabaseCommand<TResult> command)
        {
            Guard.AgainstNull(() => command, command);
            this.ThrowIfNotActive("Execute command");
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public virtual void Start()
        {
            if (this.IsActive)
            {
                const string Message = "Cannot start again because it is already active.";
                this.Logger.Error(Message);
                throw new HexagonException(Message);
            }

            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug("Starting.");
            }

            this.transaction = this.Database.BeginTransaction(this.transactionIsolationLevel);
        }

        /// <inheritdoc />
        public void Commit()
        {
            this.ThrowIfNotActive("Commit");

            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug("Committing.");
            }

            this.SaveChanges();
            this.transaction.Commit();
            this.transaction = null;

            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug("Committed.");
            }
        }

        /// <summary>
        /// Throws a <see cref="HexagonException"/> if the <see cref="IUnitOfWork"/> is not active.
        /// </summary>
        /// <param name="action">
        /// The action name.
        /// </param>
        /// <exception cref="HexagonException">
        /// If it is not active.
        /// </exception>
        protected void ThrowIfNotActive(string action)
        {
            if (!this.IsActive)
            {
                var message = string.Format(@"Unable to perform {0} because the unit of work is not active. Try to Start it first, or not commit it before.", action);
                this.Logger.Error(message);
                throw new HexagonException(message);
            }
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.transaction != null)
                {
                    this.transaction.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}
