// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestionMap.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Mapping class for <see cref="Question" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StackOverflow.Database.Mappings
{
    using StackOverflow.Entities;

    /// <summary>
    /// Mapping class for <see cref="Question"/>.
    /// </summary>
    public class QuestionMap : EntityMap<Question>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionMap"/> class.
        /// </summary>
        public QuestionMap()
        {
            this.Property(x => x.Title);
            this.Property(x => x.Text);
        }
    }
}
