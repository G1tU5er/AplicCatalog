namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addanscolar : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlanInvatamantMateries", newName: "MateriePlanInvatamants");
            DropPrimaryKey("dbo.MateriePlanInvatamants");
            AddColumn("dbo.PlanInvatamants", "IdAnScolar", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MateriePlanInvatamants", new[] { "Materie_IdMaterie", "PlanInvatamant_IdPlan" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MateriePlanInvatamants");
            DropColumn("dbo.PlanInvatamants", "IdAnScolar");
            AddPrimaryKey("dbo.MateriePlanInvatamants", new[] { "PlanInvatamant_IdPlan", "Materie_IdMaterie" });
            RenameTable(name: "dbo.MateriePlanInvatamants", newName: "PlanInvatamantMateries");
        }
    }
}
