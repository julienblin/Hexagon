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
        /// The type factory.
        /// </summary>
        private readonly ITypeFactory factory;

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
        /// </summary>
        /// <param name="factory">
        /// The type factory.
        /// </param>
        public DbContextUnitOfWorkRepository(ITypeFactory factory)
        {
            Guard.AgainstNull(() => factory, factory);
            this.factory = factory;
            this.SetupLog();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextUnitOfWorkRepository"/> class.
        /// Uses <see cref="IsolationLevel.ReadCommitted"/> for transactions.
        /// </summary>
        /// <param name="factory">
        /// The type factory.
        /// </param>
        /// <param name="nameOrConnectionString">
        /// Either the database name or a connection string.
        /// </param>
        public DbContextUnitOfWorkRepository(ITypeFactory factory, string nameOrConnectionString)
            : this(factory, nameOrConnectionString, IsolationLevel.ReadCommitted)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextUnitOfWorkRepository"/> class.
        /// </summary>
        /// <param name="factory">
        /// The type factory.
        /// </param>
        /// <param name="nameOrConnectionString">
        /// Either the database name or a connection string.
        /// </param>
        /// <param name="transactionIsolationLevel">
        /// The transaction isolation level.
        /// </param>
        public DbContextUnitOfWorkRepository(ITypeFactory factory, string nameOrConnectionString, IsolationLevel transactionIsolationLevel)
            : base(nameOrConnectionString)
        {
            Guard.AgainstNull(() => factory, factory);
            Guard.AgainstNullOrEmpty(() => nameOrConnectionString, nameOrConnectionString);
            this.factory = factory;
            this.transactionIsolationLevel = transactionIsolationLevel;
            this.SetupLog();
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

            // Flush pending changes, but since we manage the transaction it is still transactionnal.
            this.SaveChanges();

            var handlerType = typeof(IDbContextDatabaseQueryHandler<,>).MakeGenericType(
                query.GetType(),
                typeof(TResult));
            var handler = this.factory.Get<IDbContextDatabaseQueryHandler>(handlerType);

            if (handler == null)
            {
                throw new HexagonException(string.Format("Unable to find an appropriate handler for {0} using type definition {1}.", query, handlerType));
            }

            try
            {
                var result = handler.Handle(query, this);
                if (!(result is TResult))
                {
                    var message =
                        string.Format("Processing error: invalid result type for query {0}. Expected {1}, got {2}", query, typeof(TResult), result.GetType());
                    this.Logger.Error(message);
                    throw new HexagonException(message);
                }

                return (TResult)result;
            }
            finally
            {
                this.factory.Release(handler);
            }
        }

        /// <inheritdoc />
        public virtual void Execute(IDatabaseCommand command)
        {
            Guard.AgainstNull(() => command, command);
            this.ThrowIfNotActive("Execute command");

            // Flush pending changes, but since we manage the transaction it is still transactionnal.
            this.SaveChanges();

            var handlerType = typeof(IDbContextDatabaseCommandHandler<>).MakeGenericType(command.GetType());
            var handler = this.factory.Get<IDbContextDatabaseCommandHandler>(handlerType);

            if (handler == null)
            {
                throw new HexagonException(string.Format("Unable to find an appropriate handler for {0} using type definition {1}.", command, handlerType));
            }

            try
            {
                handler.Handle(command, this);
            }
            finally
            {
                this.factory.Release(handler);
            }
        }

        /// <inheritdoc />
        public virtual TResult Execute<TResult>(IDatabaseCommand<TResult> command)
        {
            Guard.AgainstNull(() => command, command);
            this.ThrowIfNotActive("Execute command");
            
            // Flush pending changes, but since we manage the transaction it is still transactionnal.
            this.SaveChanges();

            var handlerType = typeof(IDbContextDatabaseCommandHandler<>).MakeGenericType(command.GetType());
            var handler = this.factory.Get<IDbContextDatabaseCommandHandler>(handlerType);

            if (handler == null)
            {
                throw new HexagonException(string.Format("Unable to find an appropriate handler for {0} using type definition {1}.", command, handlerType));
            }

            try
            {
                var result = handler.Handle(command, this);
                if (!(result is TResult))
                {
                    var message =
                        string.Format("Processing error: invalid result type for command {0}. Expected {1}, got {2}", command, typeof(TResult), result.GetType());
                    this.Logger.Error(message);
                    throw new HexagonException(message);
                }

                return (TResult)result;
            }
            finally
            {
                this.factory.Release(handler);
            }
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

        /// <summary>
        /// Hooks up the entity framework SQL log.
        /// </summary>
        private void SetupLog()
        {
            this.Database.Log += log =>
                {
                    if (this.Logger.IsDebugEnabled)
                    {
                        this.Logger.Debug(log);
                    }
                };
        }
    }
}
