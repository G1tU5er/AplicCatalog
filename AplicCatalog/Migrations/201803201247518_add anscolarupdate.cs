namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addanscolarupdate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlanInvatamantMateries", newName: "MateriePlanInvatamants");
            DropPrimaryKey("dbo.MateriePlanInvatamants");
            AddPrimaryKey("dbo.MateriePlanInvatamants", new[] { "Materie_IdMaterie", "PlanInvatamant_IdPlan" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MateriePlanInvatamants");
            AddPrimaryKey("dbo.MateriePlanInvatamants", new[] { "PlanInvatamant_IdPlan", "Materie_IdMaterie" });
            RenameTable(name: "dbo.MateriePlanInvatamants", newName: "PlanInvatamantMateries");
        }
    }
}
