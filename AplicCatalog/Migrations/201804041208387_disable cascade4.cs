namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disablecascade4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            AddForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie");
            AddForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            AddForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan", cascadeDelete: true);
            AddForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie", cascadeDelete: true);
        }
    }
}
