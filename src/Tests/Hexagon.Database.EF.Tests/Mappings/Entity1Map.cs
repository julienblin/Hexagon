// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Entity1Map.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Defines the Entity1Map type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests.Mappings
{
    using System.Data.Entity.ModelConfiguration;

    using Hexagon.Database.EF.Tests.Entities;

    public class Entity1Map : EntityTypeConfiguration<Entity1>
    {
        public Entity1Map()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.Value);
        }
    }
}
