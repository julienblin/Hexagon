// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AskQuestionHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Handler for <see cref="AskQuestionCommand" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StackOverflow.Handlers
{
    using Hexagon;
    using Hexagon.Database;

    using StackOverflow.Entities;
    using StackOverflow.Messages.Questions;

    /// <summary>
    /// Handler for <see cref="AskQuestionCommand"/>.
    /// </summary>
    public class AskQuestionHandler : RequestHandler<AskQuestionCommand, AskQuestionResponse>
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IDatabaseRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AskQuestionHandler"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public AskQuestionHandler(IDatabaseRepository repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc />
        protected override void Handle(AskQuestionCommand request, AskQuestionResponse response)
        {
            var newQuestion = new Question { Title = request.Title, Text = request.Text };
            this.repository.Add(newQuestion);
            response.QuestionId = newQuestion.Id;
        }
    }
}
