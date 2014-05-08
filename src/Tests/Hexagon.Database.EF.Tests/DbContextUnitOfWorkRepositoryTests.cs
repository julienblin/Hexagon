namespace Hexagon.Database.EF.Tests
{
    using Hexagon.Database.EF.Tests.Command;
    using Hexagon.Database.EF.Tests.Entities;
    using Hexagon.Database.EF.Tests.Queries;
    using Hexagon.Impl;

    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    [TestFixture]
    public class DbContextUnitOfWorkRepositoryTests
    {
        private DbContextUnitOfWorkRepository context;

        [SetUp]
        public void SetUp()
        {
            this.context = new DbContextUnitOfWorkRepository { Logger = TestConsoleLogger.Instance };
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }

        [Test]
        public void MostOperationsShouldThrow_IfInactive()
        {
            var assertion = new ReusableConstraint(Throws.Exception.TypeOf<HexagonException>().With.Message.ContainsSubstring("not active"));
            Assert.That(() => this.context.Get<Entity1>(1), assertion);
            Assert.That(() => this.context.Add(new Entity1()), assertion);
            Assert.That(() => this.context.Remove(new Entity1()), assertion);
            Assert.That(() => this.context.Execute(new Entity1Query()), assertion);
            Assert.That(() => this.context.Execute(new CommandNoResult()), assertion);
            Assert.That(() => this.context.Execute(new CommandWithResult()), assertion);
            Assert.That(() => this.context.Commit(), assertion);
        }

        [Test]
        public void ItShouldStartAndCommit_WithNoProblem()
        {
            Assert.That(() => this.context.Start(), Throws.Nothing);
            Assert.That(() => this.context.Commit(), Throws.Nothing);
        }
    }
}
