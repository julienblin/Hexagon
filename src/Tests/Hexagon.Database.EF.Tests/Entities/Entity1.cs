// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Entity1.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Defines the Entity1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Entity1
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
