namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddirigprop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncadrareProfs", "EDiriginte", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncadrareProfs", "EDiriginte");
        }
    }
}
