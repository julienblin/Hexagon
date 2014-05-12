// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AskQuestionCommand.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Command to ask a new question
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StackOverflow.Messages.Questions
{
    using System.ComponentModel.DataAnnotations;

    using Hexagon.Messages;

    /// <summary>
    /// Command to ask a new question
    /// </summary>
    public class AskQuestionCommand : Command<AskQuestionResponse>
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Required]
        [MaxLength(300)]
        [MinLength(15)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [Required]
        public string Text { get; set; }
    }
}
