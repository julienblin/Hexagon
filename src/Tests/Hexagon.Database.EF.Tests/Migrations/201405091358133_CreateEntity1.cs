// --------------------------------------------------------------------------------------------------------------------
// <copyright file="201405091358133_CreateEntity1.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Defines the CreateEntity1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// The create entity 1.
    /// </summary>
    public partial class CreateEntity1 : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Entity1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);   
        }
        
        public override void Down()
        {
            this.DropTable("dbo.Entity1");
        }
    }
}
