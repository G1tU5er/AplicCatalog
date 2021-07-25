namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateplan1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Materies", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropIndex("dbo.Materies", new[] { "PlanInvatamant_IdPlan" });
            AddColumn("dbo.PlanInvatamants", "Materie_IdMaterie", c => c.Int());
            CreateIndex("dbo.PlanInvatamants", "Materie_IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie");
            DropColumn("dbo.Materies", "PlanInvatamant_IdPlan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materies", "PlanInvatamant_IdPlan", c => c.Int());
            DropForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropIndex("dbo.PlanInvatamants", new[] { "Materie_IdMaterie" });
            DropColumn("dbo.PlanInvatamants", "Materie_IdMaterie");
            CreateIndex("dbo.Materies", "PlanInvatamant_IdPlan");
            AddForeignKey("dbo.Materies", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan");
        }
    }
}
