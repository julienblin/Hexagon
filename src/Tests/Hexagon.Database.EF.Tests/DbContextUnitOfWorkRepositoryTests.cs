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
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Hexagon.Database.EF.Tests.CommandHandlers;
    using Hexagon.Database.EF.Tests.Commands;
    using Hexagon.Database.EF.Tests.Entities;
    using Hexagon.Database.EF.Tests.Queries;
    using Hexagon.Database.EF.Tests.QueryHandlers;
    using Hexagon.Impl;
    using Hexagon.Messages;

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
        public void Execute_ShouldExecuteARealQueryHandler_UsingFirstOrDefault()
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
                context.Add(new Entity1 { Value = "Something Good" });

                var result = context.Execute(query).FirstOrDefault();

                Assert.That(() => result.Value, Is.EqualTo("Something Good"));
            }
        }

        [Test]
        public void Execute_ShouldExecuteARealQueryHandler_UsingList()
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
                context.Add(new Entity1 { Value = "Something Good" });

                var result = context.Execute(query).List();

                Assert.That(() => result, Has.Count.EqualTo(1));
            }
        }

        [Test]
        public void Execute_ShouldExecuteARealQueryHandler_UsingPaginate()
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
                for (var i = 0; i < 50; i++)
                {
                    context.Add(new Entity1 { Value = "Something Good " + i });
                }

                var intResult = context.Execute(query).OrderBy(x => x.Value);
                var resultPage1 = intResult.Paginate(1, 30);
                var resultPage2 = intResult.Paginate(2, 30);

                Assert.That(resultPage1.CurrentPage, Is.EqualTo(1));
                Assert.That(resultPage1.PerPage, Is.EqualTo(30));
                Assert.That(resultPage1.TotalEntries, Is.EqualTo(50));
                Assert.That(resultPage1.PageCount(), Is.EqualTo(2));
                Assert.That(resultPage1.Items.Count(), Is.EqualTo(30));

                Assert.That(resultPage2.CurrentPage, Is.EqualTo(2));
                Assert.That(resultPage2.PerPage, Is.EqualTo(30));
                Assert.That(resultPage2.TotalEntries, Is.EqualTo(50));
                Assert.That(resultPage2.PageCount(), Is.EqualTo(2));
                Assert.That(resultPage2.Items.Count(), Is.EqualTo(20));

                Assert.That(resultPage2.Items.Last().Value, Is.StringEnding(9.ToString())); // Because of sort is not using natural sort :-(
            }
        }

        [Test]
        public void Execute_ShouldExecuteARealQueryHandler_UsingCount()
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
                for (var i = 0; i < 20; i++)
                {
                    context.Add(new Entity1 { Value = "Something Good " + i });
                }

                var result = context.Execute(query).Count();

                Assert.That(result, Is.EqualTo(20));
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

        [Test]
        public void Execute_ShouldSelectCommandHandler_AndInvokeHandle_WithNoResult()
        {
            var command = new CommandNoResult();
            var commandHandlerMock = new Mock<IDbContextDatabaseCommandHandler<CommandNoResult>>();
            this.factoryMock.Setup(x => x.Get<IDbContextDatabaseCommandHandler>(typeof(IDbContextDatabaseCommandHandler<CommandNoResult>)))
                            .Returns(commandHandlerMock.Object)
                            .Verifiable();

            using (var context = this.CreateContext())
            {
                context.Start();
                context.Execute(command);
                commandHandlerMock.Verify(x => x.Handle(command, It.IsNotNull<DbContext>()));
                this.factoryMock.Verify();
            }
        }

        [Test]
        public void Execute_ShouldSelectCommandHandler_AndInvokeHandle_WithResult()
        {
            var command = new CommandWithResult();
            var expectedResult = new DateTime();
            var commandHandlerMock = new Mock<IDbContextDatabaseCommandHandler<CommandWithResult>>();
            commandHandlerMock.Setup(x => x.Handle(command, It.IsNotNull<DbContext>()))
                              .Returns(expectedResult)
                              .Verifiable();
            this.factoryMock.Setup(x => x.Get<IDbContextDatabaseCommandHandler>(typeof(IDbContextDatabaseCommandHandler<CommandWithResult>)))
                            .Returns(commandHandlerMock.Object)
                            .Verifiable();

            using (var context = this.CreateContext())
            {
                context.Start();
                var result = context.Execute(command);
                Assert.That(result, Is.EqualTo(expectedResult));
                commandHandlerMock.Verify();
                this.factoryMock.Verify();
            }
        }

        [Test]
        public void Execute_ShouldThrowException_WhenNoCommandHandlerFound()
        {
            var commandNoResult = new CommandNoResult();
            var commandWithResult = new CommandWithResult();

            using (var context = this.CreateContext())
            {
                context.Start();
                Assert.That(
                    () => context.Execute(commandNoResult),
                    Throws.Exception.TypeOf<HexagonException>().With.Message.ContainsSubstring("appropriate handler"));
                Assert.That(
                    () => context.Execute(commandWithResult),
                    Throws.Exception.TypeOf<HexagonException>().With.Message.ContainsSubstring("appropriate handler"));
            }
        }

        [Test]
        public void Execute_ShouldExecuteARealCommandHandler_WithNoResult()
        {
            var command = new CommandNoResult();
            this.factoryMock.Setup(
                x =>
                x.Get<IDbContextDatabaseCommandHandler>(
                    typeof(IDbContextDatabaseCommandHandler<CommandNoResult>)))
                .Returns(new CommandNoResultHandler());

            using (var context = this.CreateContext())
            {
                context.Start();

                Assert.That(() => context.Execute(command), Throws.Nothing);
            }
        }

        [Test]
        public void Execute_ShouldExecuteARealCommandHandler_WithResult()
        {
            var command = new CommandWithResult();
            this.factoryMock.Setup(
                x =>
                x.Get<IDbContextDatabaseCommandHandler>(
                    typeof(IDbContextDatabaseCommandHandler<CommandWithResult>)))
                .Returns(new CommandWithResultHandler());

            using (var context = this.CreateContext())
            {
                context.Start();

                var result = context.Execute(command);

                Assert.That(result, Is.AtLeast(DateTime.Now.AddYears(-1)));
            }
        }

        private DbContextUnitOfWorkRepository CreateContext()
        {
            return new TestDbContext(this.factoryMock.Object) { Logger = TestConsoleLogger.Instance };
        }
    }
}
