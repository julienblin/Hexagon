// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestionQueryHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="QuestionQuery" /> handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StackOverflow.Database.QueryHandlers
{
    using System.Data.Entity;
    using System.Linq;

    using Hexagon.Database;
    using Hexagon.Database.EF;

    using StackOverflow.Entities;
    using StackOverflow.Queries;

    /// <summary>
    /// <see cref="QuestionQuery"/> handler.
    /// </summary>
    public class QuestionQueryHandler : DbContextDatabaseQueryHandler<QuestionQuery, IDatabaseQueryParametrizedResult<Question>>
    {
        /// <inheritdoc />
        protected override IDatabaseQueryParametrizedResult<Question> Handle(QuestionQuery query, DbContext context)
        {
            var set = context.Set<Question>();

            if (string.IsNullOrEmpty(query.TitleLike))
            {
                set.Where(x => x.Title.Contains(query.TitleLike));
            }

            if (string.IsNullOrEmpty(query.TextLike))
            {
                set.Where(x => x.Title.Contains(query.TextLike));
            }

            return new DbSetParametrizedResult<Question>(set);
        }
    }
}
