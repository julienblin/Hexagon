// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AskQuestionResponse.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Response for <see cref="AskQuestionCommand" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StackOverflow.Messages.Questions
{
    using Hexagon.Messages;

    /// <summary>
    /// Response for <see cref="AskQuestionCommand"/>.
    /// </summary>
    public class AskQuestionResponse : ValidatedResponse
    {
        /// <summary>
        /// Gets or sets the question id. 0 if question was not created.
        /// </summary>
        public int QuestionId { get; set; }
    }
}
