// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Entity1QueryHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="Entity1Query" /> handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests.QueryHandlers
{
    using System.Data.Entity;
    using System.Linq;

    using Hexagon.Database.EF.Tests.Entities;
    using Hexagon.Database.EF.Tests.Queries;

    /// <summary>
    /// <see cref="Entity1Query"/> handler.
    /// </summary>
    public class Entity1QueryHandler : DbContextDatabaseQueryHandler<Entity1Query, IDatabaseQueryParametrizedResult<Entity1>>
    {
        protected override IDatabaseQueryParametrizedResult<Entity1> Handle(Entity1Query query, DbContext context)
        {
            var set = context.Set<Entity1>();

            if (!string.IsNullOrEmpty(query.ValueLike))
            {
                set.Where(x => x.Value.Contains(query.ValueLike));
            }

            return new DbSetParametrizedResult<Entity1>(set);
        }
    }
}
