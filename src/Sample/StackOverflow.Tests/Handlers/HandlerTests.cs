namespace StackOverflow.Tests.Handlers
{
    using System;
    using System.Linq;

    using Hexagon;
    using Hexagon.Database;
    using Hexagon.Impl;
    using Hexagon.Local;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public abstract class HandlerTests
    {
        protected Mock<IDatabaseRepository> RepositoryMock { get; private set; }

        protected Mock<ITypeFactory> FactoryMock { get; private set; }

        protected IRequestHandler Handler { get; private set; }

        protected IRequestProcessor Processor { get; private set; }

        [SetUp]
        public virtual void SetUp()
        {
            this.RepositoryMock = new Mock<IDatabaseRepository>();
            this.Handler = this.CreateHandler();
            this.FactoryMock = new Mock<ITypeFactory>();
            this.FactoryMock.Setup(x => x.Get<IRequestHandler>(It.IsAny<Type>())).Returns(this.Handler);

            this.Processor = new LocalRequestProcessor(this.FactoryMock.Object, new[] { new DataAnnotationValidationInterceptor() });
        }

        protected abstract IRequestHandler CreateHandler();
    }
}