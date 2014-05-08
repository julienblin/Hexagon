// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerExtensionsTests.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Unit tests for <see cref="LoggerExtensions" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Tests
{
    using System;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// Unit tests for <see cref="LoggerExtensions"/>.
    /// </summary>
    [TestFixture]
    public class LoggerExtensionsTests
    {
        private Mock<ILogger> loggerMock;

        [SetUp]
        public void SetUp()
        {
            this.loggerMock = new Mock<ILogger>();
        }

        [Test]
        public void Debug_ShouldThrowNothing_WhenFormatIsInvalid()
        {
            Assert.That(() => this.loggerMock.Object.Debug("{0} {1} original.", new object()), Throws.Nothing);
            this.loggerMock.Verify(x => x.Warn(It.IsAny<FormatException>(), It.Is<string>(s => s.Contains("format") && s.Contains("original."))));
        }

        [Test]
        public void Info_ShouldThrowNothing_WhenFormatIsInvalid()
        {
            Assert.That(() => this.loggerMock.Object.Info("{0} {1} original.", new object()), Throws.Nothing);
            this.loggerMock.Verify(x => x.Warn(It.IsAny<FormatException>(), It.Is<string>(s => s.Contains("format") && s.Contains("original."))));
        }

        [Test]
        public void Warn_ShouldThrowNothing_WhenFormatIsInvalid()
        {
            Assert.That(() => this.loggerMock.Object.Warn("{0} {1} original.", new object()), Throws.Nothing);
            this.loggerMock.Verify(x => x.Warn(It.IsAny<FormatException>(), It.Is<string>(s => s.Contains("format") && s.Contains("original."))));
        }

        [Test]
        public void Error_ShouldThrowNothing_WhenFormatIsInvalid()
        {
            Assert.That(() => this.loggerMock.Object.Error("{0} {1} original.", new object()), Throws.Nothing);
            this.loggerMock.Verify(x => x.Warn(It.IsAny<FormatException>(), It.Is<string>(s => s.Contains("format") && s.Contains("original."))));
        }

        [Test]
        public void Fatal_ShouldThrowNothing_WhenFormatIsInvalid()
        {
            Assert.That(() => this.loggerMock.Object.Fatal("{0} {1} original.", new object()), Throws.Nothing);
            this.loggerMock.Verify(x => x.Warn(It.IsAny<FormatException>(), It.Is<string>(s => s.Contains("format") && s.Contains("original."))));
        }
    }
}
