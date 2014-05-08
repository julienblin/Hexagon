// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalRequestProcessorTests.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Unit tests for <see cref="LocalRequestProcessor" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Local.Tests
{
    using System;

    using Hexagon.Messages;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// Unit tests for <see cref="LocalRequestProcessor"/>.
    /// </summary>
    [TestFixture]
    public class LocalRequestProcessorTests
    {
        private Mock<ITypeFactory> typeFactoryMock;
        private Mock<IRequestProcessorInterceptor> interceptorMock;
        private IRequestProcessor processor;

        [SetUp]
        public void SetUp()
        {
            this.typeFactoryMock = new Mock<ITypeFactory>();
            this.interceptorMock = new Mock<IRequestProcessorInterceptor>();
            this.interceptorMock.SetupGet(x => x.InterceptionPriority).Returns(InterceptionPrority.Medium);
            this.interceptorMock.Setup(x => x.Intercept(It.IsAny<IRequestProcessorInvocation>()))
                .Callback<IRequestProcessorInvocation>(i => i.Proceed());
            this.processor = new LocalRequestProcessor(this.typeFactoryMock.Object, new[] { this.interceptorMock.Object }) { Logger = TestConsoleLogger.Instance };
        }

        [Test]
        public void Process_ShouldCallInterceptorAndHandler_WhenEverythingIsFine()
        {
            var requestHandler = new Mock<IRequestHandler<TestQuery, TestResponse>>();
            this.typeFactoryMock.Setup(x => x.Get<IRequestHandler>(It.IsAny<Type>()))
                                .Returns(requestHandler.Object);
            var query = new TestQuery();

            var response = this.processor.Process(query);
            
            Assert.That(response, Is.TypeOf<TestResponse>());
            Assert.That(response.Context.CorrelationId, Is.EqualTo(query.Context.CorrelationId));
            Assert.That(response.Context.Headers[InternalHeaderKeys.LocalProcessingTime], Is.Not.Empty);
            Assert.That(response.Context.Headers[InternalHeaderKeys.HandlerMachineName], Is.EqualTo(Environment.MachineName));
            requestHandler.Verify(x => x.Handle(query, response), Times.Once);
            this.interceptorMock.Verify(x => x.Intercept(It.IsAny<IRequestProcessorInvocation>()), Times.Once);
        }

        [Test]
        public void Process_ShouldNotCallHandler_WhenInterceptorIntercepts()
        {
            var requestHandler = new Mock<IRequestHandler<TestQuery, TestResponse>>();
            this.typeFactoryMock.Setup(x => x.Get<IRequestHandler>(It.IsAny<Type>()))
                                .Returns(requestHandler.Object);
            this.interceptorMock.Setup(x => x.Intercept(It.IsAny<IRequestProcessorInvocation>()));

            var query = new TestQuery();

            this.processor.Process(query);
            requestHandler.Verify(x => x.Handle(query, It.IsAny<IResponse>()), Times.Never);
            this.interceptorMock.Verify(x => x.Intercept(It.IsAny<IRequestProcessorInvocation>()), Times.Once);
        }

        [Test]
        public void Process_ShouldThrowException_WhenInterceptorReturnsBadResponseType()
        {
            var requestHandler = new Mock<IRequestHandler<TestQuery, TestResponse>>();
            this.typeFactoryMock.Setup(x => x.Get<IRequestHandler>(It.IsAny<Type>()))
                                .Returns(requestHandler.Object);
            this.interceptorMock.Setup(x => x.Intercept(It.IsAny<IRequestProcessorInvocation>()))
                                .Callback<IRequestProcessorInvocation>(i => i.Response = new AnotherTestResponse());

            var query = new TestQuery();

            Assert.That(
                () => this.processor.Process(query),
                Throws.Exception.TypeOf<HexagonException>().With.InnerException.Message.ContainsSubstring("invalid response type"));
        }

        [Test]
        public async void ProcessAsync_ShouldCallInterceptorAndHandler_WhenEverythingIsFine()
        {
            var requestHandler = new Mock<IRequestHandler<TestQuery, TestResponse>>();
            this.typeFactoryMock.Setup(x => x.Get<IRequestHandler>(It.IsAny<Type>()))
                                .Returns(requestHandler.Object);
            var query = new TestQuery();

            var response = await this.processor.ProcessAsync(query);

            Assert.That(response, Is.TypeOf<TestResponse>());
            Assert.That(response.Context.CorrelationId, Is.EqualTo(query.Context.CorrelationId));
            Assert.That(response.Context.Headers[InternalHeaderKeys.LocalProcessingTime], Is.Not.Empty);
            Assert.That(response.Context.Headers[InternalHeaderKeys.HandlerMachineName], Is.EqualTo(Environment.MachineName));
            requestHandler.Verify(x => x.Handle(query, response), Times.Once);
            this.interceptorMock.Verify(x => x.Intercept(It.IsAny<IRequestProcessorInvocation>()), Times.Once);
        }

        [Test]
        public async void ProcessAsync_ShouldNotCallHandler_WhenInterceptorIntercepts()
        {
            var requestHandler = new Mock<IRequestHandler<TestQuery, TestResponse>>();
            this.typeFactoryMock.Setup(x => x.Get<IRequestHandler>(It.IsAny<Type>()))
                                .Returns(requestHandler.Object);
            this.interceptorMock.Setup(x => x.Intercept(It.IsAny<IRequestProcessorInvocation>()));

            var query = new TestQuery();

            await this.processor.ProcessAsync(query);
            requestHandler.Verify(x => x.Handle(query, It.IsAny<IResponse>()), Times.Never);
            this.interceptorMock.Verify(x => x.Intercept(It.IsAny<IRequestProcessorInvocation>()), Times.Once);
        }

        [Test]
        public void ProcessAsync_ShouldThrowException_WhenInterceptorReturnsBadResponseType()
        {
            var requestHandler = new Mock<IRequestHandler<TestQuery, TestResponse>>();
            this.typeFactoryMock.Setup(x => x.Get<IRequestHandler>(It.IsAny<Type>()))
                                .Returns(requestHandler.Object);
            this.interceptorMock.Setup(x => x.Intercept(It.IsAny<IRequestProcessorInvocation>()))
                                .Callback<IRequestProcessorInvocation>(i => i.Response = new AnotherTestResponse());

            var query = new TestQuery();

            Assert.That(
                async () => await this.processor.ProcessAsync(query),
                Throws.Exception.TypeOf<HexagonException>().With.InnerException.Message.ContainsSubstring("invalid response type"));
        }

        public class TestResponse : Response
        {
        }

        public class AnotherTestResponse : Response
        {
        }

        public class TestQuery : Query<TestResponse>
        {
        }
    }
}
