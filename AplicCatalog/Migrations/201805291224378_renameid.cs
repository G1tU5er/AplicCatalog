namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameid : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.IncadrareMateriePlans", new[] { "PlanInvatamant_IdPlan" });
            RenameColumn(table: "dbo.IncadrareMateriePlans", name: "PlanInvatamant_IdPlan", newName: "IdPlan");
            AlterColumn("dbo.IncadrareMateriePlans", "IdPlan", c => c.Int(nullable: false));
            CreateIndex("dbo.IncadrareMateriePlans", "IdPlan");
            DropColumn("dbo.IncadrareMateriePlans", "idPlanInv");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IncadrareMateriePlans", "idPlanInv", c => c.Int(nullable: false));
            DropIndex("dbo.IncadrareMateriePlans", new[] { "IdPlan" });
            AlterColumn("dbo.IncadrareMateriePlans", "IdPlan", c => c.Int());
            RenameColumn(table: "dbo.IncadrareMateriePlans", name: "IdPlan", newName: "PlanInvatamant_IdPlan");
            CreateIndex("dbo.IncadrareMateriePlans", "PlanInvatamant_IdPlan");
        }
    }
}
