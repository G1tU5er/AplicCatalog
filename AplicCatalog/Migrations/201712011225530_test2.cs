namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PlanInvatamants", new[] { "an_IdAn" });
            CreateIndex("dbo.PlanInvatamants", "An_IdAn");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PlanInvatamants", new[] { "An_IdAn" });
            CreateIndex("dbo.PlanInvatamants", "an_IdAn");
        }
    }
}
