namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addidplan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clasas", "PlanDeInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropIndex("dbo.Clasas", new[] { "PlanDeInvatamant_IdPlan" });
            RenameColumn(table: "dbo.Clasas", name: "PlanDeInvatamant_IdPlan", newName: "IdPlan");
            AlterColumn("dbo.Clasas", "IdPlan", c => c.Int(nullable: false));
            CreateIndex("dbo.Clasas", "IdPlan");
            AddForeignKey("dbo.Clasas", "IdPlan", "dbo.PlanInvatamants", "IdPlan", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clasas", "IdPlan", "dbo.PlanInvatamants");
            DropIndex("dbo.Clasas", new[] { "IdPlan" });
            AlterColumn("dbo.Clasas", "IdPlan", c => c.Int());
            RenameColumn(table: "dbo.Clasas", name: "IdPlan", newName: "PlanDeInvatamant_IdPlan");
            CreateIndex("dbo.Clasas", "PlanDeInvatamant_IdPlan");
            AddForeignKey("dbo.Clasas", "PlanDeInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan");
        }
    }
}
