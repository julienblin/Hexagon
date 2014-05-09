// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestDbContext.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Local <see cref="DbContext" /> to play by the EF rules ;-)
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests
{
    using System.Data.Entity;

    /// <summary>
    /// Local <see cref="DbContext"/> to play by the EF rules ;-)
    /// </summary>
    public class TestDbContext : DbContextUnitOfWorkRepository
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(TestDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
