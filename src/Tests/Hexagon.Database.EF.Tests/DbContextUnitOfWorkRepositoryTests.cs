// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbContextUnitOfWorkRepositoryTests.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Unit tests for <see cref="DbContextUnitOfWorkRepository"/>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests
{
    using Hexagon.Database.EF.Tests.Command;
    using Hexagon.Database.EF.Tests.Entities;
    using Hexagon.Database.EF.Tests.Queries;
    using Hexagon.Impl;

    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    /// <summary>
    /// Unit tests for <see cref="DbContextUnitOfWorkRepository"/>.
    /// </summary>
    [TestFixture]
    public class DbContextUnitOfWorkRepositoryTests
    {
        [Test]
        public void MostOperationsShouldThrow_IfInactive()
        {
            using (var context = this.CreateContext())
            {
                var assertion = new ReusableConstraint(Throws.Exception.TypeOf<HexagonException>().With.Message.ContainsSubstring("not active"));
                Assert.That(() => context.Get<Entity1>(1), assertion);
                Assert.That(() => context.Add(new Entity1()), assertion);
                Assert.That(() => context.Remove(new Entity1()), assertion);
                Assert.That(() => context.Execute(new Entity1Query()), assertion);
                Assert.That(() => context.Execute(new CommandNoResult()), assertion);
                Assert.That(() => context.Execute(new CommandWithResult()), assertion);
                Assert.That(() => context.Commit(), assertion);
            }
        }

        [Test]
        public void ItShouldStartAndCommit_WithNoProblem()
        {
            using (var context = this.CreateContext())
            {
                Assert.That(() => context.Start(), Throws.Nothing);
                Assert.That(() => context.Commit(), Throws.Nothing);
            }
        }

        [Test]
        public void ItShouldAddGetAndRemove_WithNoProblem()
        {
            var entity = new Entity1 { Value = "TheValue" };
            using (var context = this.CreateContext())
            {
                context.Start();
                context.Add(entity);
                context.Commit();
            }

            using (var context = this.CreateContext())
            {
                context.Start();
                var result = context.Get<Entity1>(entity.Id);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value, Is.EqualTo(entity.Value));

                context.Remove(result);
                Assert.That(() => context.Commit(), Throws.Nothing);
            }
        }

        private DbContextUnitOfWorkRepository CreateContext()
        {
            return new TestDbContext { Logger = TestConsoleLogger.Instance };
        }
    }
}
