namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateplan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnScolars", "PlanInvatamant_IdPlan", c => c.Int());
            CreateIndex("dbo.AnScolars", "PlanInvatamant_IdPlan");
            AddForeignKey("dbo.AnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropIndex("dbo.AnScolars", new[] { "PlanInvatamant_IdPlan" });
            DropColumn("dbo.AnScolars", "PlanInvatamant_IdPlan");
        }
    }
}
