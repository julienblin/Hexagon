// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestionQuery.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Standard database query for <see cref="Question" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StackOverflow.Queries
{
    using Hexagon.Database;

    using StackOverflow.Entities;

    /// <summary>
    /// Standard database query for <see cref="Question"/>.
    /// </summary>
    public class QuestionQuery : IParametrizedDatabaseQuery<Question>
    {
        /// <summary>
        /// Gets or sets the <see cref="Question.Title"/> LIKE query parameter.
        /// </summary>
        public string TitleLike { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Question.Text"/> LIKE query parameter.
        /// </summary>
        public string TextLike { get; set; }
    }
}
