// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackOverflowContext.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Local context to play by the rule of EF.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StackOverflow.Database
{
    using System.Data.Entity;

    using Hexagon;
    using Hexagon.Database.EF;

    /// <summary>
    /// Local context to play by the rule of EF.
    /// </summary>
    public class StackOverflowContext : DbContextUnitOfWorkRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StackOverflowContext"/> class.
        /// </summary>
        public StackOverflowContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackOverflowContext"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        public StackOverflowContext(ITypeFactory factory)
            : base(factory)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(StackOverflowContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
