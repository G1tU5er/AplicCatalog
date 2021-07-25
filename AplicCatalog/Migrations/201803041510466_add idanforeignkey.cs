namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addidanforeignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", "dbo.AnScolars");
            DropIndex("dbo.PlanInvatamants", new[] { "AnDeValabilitate_IdAn" });
            RenameColumn(table: "dbo.PlanInvatamants", name: "AnDeValabilitate_IdAn", newName: "IdAn");
            AlterColumn("dbo.PlanInvatamants", "IdAn", c => c.Int(nullable: false));
            CreateIndex("dbo.PlanInvatamants", "IdAn");
            AddForeignKey("dbo.PlanInvatamants", "IdAn", "dbo.AnScolars", "IdAn", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanInvatamants", "IdAn", "dbo.AnScolars");
            DropIndex("dbo.PlanInvatamants", new[] { "IdAn" });
            AlterColumn("dbo.PlanInvatamants", "IdAn", c => c.Int());
            RenameColumn(table: "dbo.PlanInvatamants", name: "IdAn", newName: "AnDeValabilitate_IdAn");
            CreateIndex("dbo.PlanInvatamants", "AnDeValabilitate_IdAn");
            AddForeignKey("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", "dbo.AnScolars", "IdAn");
        }
    }
}
