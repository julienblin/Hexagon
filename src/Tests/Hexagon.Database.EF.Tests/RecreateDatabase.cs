// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecreateDatabase.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Sets the EF initializer to <see cref="DropCreateDatabaseAlways{TContext}" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests
{
    using System.Data.Entity;

    using NUnit.Framework;

    /// <summary>
    /// Sets the EF initializer to <see cref="DropCreateDatabaseAlways{TContext}"/>.
    /// </summary>
    [SetUpFixture]
    public class RecreateDatabase
    {
        [SetUp]
        public void SetUp()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TestDbContext>());
        }
    }
}
