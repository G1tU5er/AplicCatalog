namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropidanfromincadrareprof : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Incadrares", "idAnScolar");
            DropColumn("dbo.IncadrareProfs", "idAnScolar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IncadrareProfs", "idAnScolar", c => c.Int(nullable: false));
            AddColumn("dbo.Incadrares", "idAnScolar", c => c.Int(nullable: false));
        }
    }
}
