// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityMap.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Base class for mappings of <see cref="Entity" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StackOverflow.Database.Mappings
{
    using System.Data.Entity.ModelConfiguration;

    using StackOverflow.Entities;

    /// <summary>
    /// Base class for mappings of <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of entity.
    /// </typeparam>
    public abstract class EntityMap<T> : EntityTypeConfiguration<T>
        where T : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMap{T}"/> class.
        /// with <see cref="Entity.Id"/> as the key.
        /// </summary>
        protected EntityMap()
        {
            this.HasKey(x => x.Id);
        }
    }
}
