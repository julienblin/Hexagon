﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbContextUnitOfWorkRepositoryTests.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Unit tests for <see cref="DbContextUnitOfWorkRepository"/>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests
{
    using System.Data.Entity;

    using Hexagon.Database.EF.Tests.Command;
    using Hexagon.Database.EF.Tests.Entities;
    using Hexagon.Database.EF.Tests.Queries;
    using Hexagon.Database.EF.Tests.QueryHandlers;
    using Hexagon.Impl;

    using Moq;

    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    /// <summary>
    /// Unit tests for <see cref="DbContextUnitOfWorkRepository"/>.
    /// </summary>
    [TestFixture]
    public class DbContextUnitOfWorkRepositoryTests
    {
        private Mock<ITypeFactory> factoryMock;

        [SetUp]
        public void SetUp()
        {
            this.factoryMock = new Mock<ITypeFactory>();
        }

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

        [Test]
        public void Execute_ShouldSelectQueryHandler_AndReturnResult()
        {
            var query = new Entity1Query();
            var resultMock = new Mock<IDatabaseQueryParametrizedResult<Entity1>>();
            var queryHandler = new Mock<IDbContextDatabaseQueryHandler<Entity1Query, IDatabaseQueryParametrizedResult<Entity1>>>();
            queryHandler.Setup(x => x.Handle(query, It.IsNotNull<DbContext>())).Returns(resultMock.Object).Verifiable();
            this.factoryMock.Setup(x => x.Get<IDbContextDatabaseQueryHandler>(typeof(IDbContextDatabaseQueryHandler<Entity1Query, IDatabaseQueryParametrizedResult<Entity1>>)))
                            .Returns(queryHandler.Object)
                            .Verifiable();

            using (var context = this.CreateContext())
            {
                context.Start();
                var result = context.Execute(query);
                Assert.That(result, Is.SameAs(resultMock.Object));
                queryHandler.Verify();
                this.factoryMock.Verify();
            }
        }

        [Test]
        public void Execute_ShouldExecuteARealQueryHandler()
        {
            var query = new Entity1Query { ValueLike = "Something" };
            this.factoryMock.Setup(
                x =>
                x.Get<IDbContextDatabaseQueryHandler>(
                    typeof(IDbContextDatabaseQueryHandler<Entity1Query, IDatabaseQueryParametrizedResult<Entity1>>)))
                .Returns(new Entity1QueryHandler());

            using (var context = this.CreateContext())
            {
                context.Start();
                Assert.That(() => context.Execute(query).List(), Throws.Nothing);
            }
        }

        [Test]
        public void Execute_ShouldThrowException_WhenNoQueryHandlerFound()
        {
            var query = new Entity1Query();

            using (var context = this.CreateContext())
            {
                context.Start();
                Assert.That(
                    () => context.Execute(query),
                    Throws.Exception.TypeOf<HexagonException>().With.Message.ContainsSubstring("appropriate handler"));
            }
        }

        private DbContextUnitOfWorkRepository CreateContext()
        {
            return new TestDbContext(this.factoryMock.Object) { Logger = TestConsoleLogger.Instance };
        }
    }
}
