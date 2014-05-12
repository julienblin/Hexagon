// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Entity.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Base class for entities.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StackOverflow.Entities
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }
    }
}
