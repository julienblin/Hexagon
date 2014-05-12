namespace StackOverflow.Tests.Handlers
{
    using Hexagon;

    using Moq;

    using NUnit.Framework;

    using StackOverflow.Entities;
    using StackOverflow.Handlers;
    using StackOverflow.Messages.Questions;

    public class AskQuestionHandlerTests : HandlerTests
    {
        protected override IRequestHandler CreateHandler()
        {
            return new AskQuestionHandler(this.RepositoryMock.Object);
        }

        [Test]
        public void ItShouldCreateQuestion()
        {
            var command = new AskQuestionCommand { Title = "TheTitleIsMoreThan15", Text = "TheText" };

            this.RepositoryMock.Setup(x => x.Add(It.IsNotNull<Question>())).Callback<Question>(x => x.Id = 1);

            var response = this.Processor.Process(command);

            Assert.That(response.IsValid);
            Assert.That(response.QuestionId, Is.EqualTo(1));
        }
    }
}
