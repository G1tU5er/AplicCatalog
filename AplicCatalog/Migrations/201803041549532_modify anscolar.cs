namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyanscolar : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MateriePlanInvatamants", newName: "PlanInvatamantMateries");
            DropPrimaryKey("dbo.PlanInvatamantMateries");
            AddPrimaryKey("dbo.PlanInvatamantMateries", new[] { "PlanInvatamant_IdPlan", "Materie_IdMaterie" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PlanInvatamantMateries");
            AddPrimaryKey("dbo.PlanInvatamantMateries", new[] { "Materie_IdMaterie", "PlanInvatamant_IdPlan" });
            RenameTable(name: "dbo.PlanInvatamantMateries", newName: "MateriePlanInvatamants");
        }
    }
}
