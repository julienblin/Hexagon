// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Question.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   The question.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StackOverflow.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The question.
    /// </summary>
    public class Question : Entity
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
