namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateplaninvatam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanInvatamants", "an_IdAn", c => c.Int());
            CreateIndex("dbo.PlanInvatamants", "an_IdAn");
            AddForeignKey("dbo.PlanInvatamants", "an_IdAn", "dbo.AnScolars", "IdAn");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanInvatamants", "an_IdAn", "dbo.AnScolars");
            DropIndex("dbo.PlanInvatamants", new[] { "an_IdAn" });
            DropColumn("dbo.PlanInvatamants", "an_IdAn");
        }
    }
}
