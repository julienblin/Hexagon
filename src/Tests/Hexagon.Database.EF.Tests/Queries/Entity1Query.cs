// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Entity1Query.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Defines the Entity1Query type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests.Queries
{
    using Hexagon.Database.EF.Tests.Entities;

    public class Entity1Query : IParametrizedDatabaseQuery<Entity1>
    {
        public string ValueLike { get; set; }
    }
}
