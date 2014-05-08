// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAnnotationValidationInterceptorTests.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Unit tests for <see cref="DataAnnotationValidationInterceptor" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Tests.Impl
{
    using System.ComponentModel.DataAnnotations;

    using Hexagon.Impl;
    using Hexagon.Messages;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// Unit tests for <see cref="DataAnnotationValidationInterceptor"/>.
    /// </summary>
    [TestFixture]
    public class DataAnnotationValidationInterceptorTests
    {
        private Mock<IRequestProcessorInvocation> invocationMock;

        private IRequestProcessorInterceptor interceptor;

        [SetUp]
        public void SetUp()
        {
            this.invocationMock = new Mock<IRequestProcessorInvocation>();
            this.interceptor = new DataAnnotationValidationInterceptor();
        }

        [Test]
        public void ShouldProceed_WhenNoValidationError()
        {
            var command = new TestCommand { RequiredValue = "value" };
            var response = new TestResponse();
            this.invocationMock.SetupGet(x => x.Request).Returns(command);
            this.invocationMock.SetupGet(x => x.Response).Returns(response);

            this.interceptor.Intercept(this.invocationMock.Object);

            this.invocationMock.Verify(x => x.Proceed(), Times.Once);
        }

        [Test]
        public void ShouldNotProceedAndThrow_WhenValidationFails_AndNoIValidationInfo()
        {
            var command = new TestCommand();
            var response = new TestResponse();
            this.invocationMock.SetupGet(x => x.Request).Returns(command);
            this.invocationMock.SetupGet(x => x.Response).Returns(response);

            Assert.That(
                () => this.interceptor.Intercept(this.invocationMock.Object),
                Throws.Exception.TypeOf<ValidationException>());

            this.invocationMock.Verify(x => x.Proceed(), Times.Never);
        }

        [Test]
        public void ShouldNotProceedAndFillResponse_WhenValidationFails_AndResponseIsIValidationInfo()
        {
            var command = new TestValidatedCommand();
            var response = new TestValidatedResponse();
            this.invocationMock.SetupGet(x => x.Request).Returns(command);
            this.invocationMock.SetupGet(x => x.Response).Returns(response);

            this.interceptor.Intercept(this.invocationMock.Object);

            this.invocationMock.Verify(x => x.Proceed(), Times.Never);
            Assert.That(response.IsValid, Is.False);
            Assert.That(response.ValidationResults, Has.Count.EqualTo(1));
        }

        public class TestResponse : Response
        {        
        }

        public class TestValidatedResponse : ValidatedResponse
        {
        }

        public class TestCommand : Command<TestResponse>
        {
            [Required]
            public string RequiredValue { get; set; }
        }

        public class TestValidatedCommand : Command<TestValidatedResponse>
        {
            [Required]
            public string RequiredValue { get; set; }
        }
    }
}
