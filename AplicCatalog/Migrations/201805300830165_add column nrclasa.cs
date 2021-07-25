namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnnrclasa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanInvatamants", "NrClasa", c => c.Int(nullable: false));
            AddColumn("dbo.Clasas", "NrClasa", c => c.Int(nullable: false));
            DropColumn("dbo.Catalogs", "NotaTeza");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Catalogs", "NotaTeza", c => c.Int());
            DropColumn("dbo.Clasas", "NrClasa");
            DropColumn("dbo.PlanInvatamants", "NrClasa");
        }
    }
}
