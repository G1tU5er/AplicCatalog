namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rollbackidan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanInvatamants", "IdAn", "dbo.AnScolars");
            DropIndex("dbo.PlanInvatamants", new[] { "IdAn" });
            RenameColumn(table: "dbo.PlanInvatamants", name: "IdAn", newName: "AnDeValabilitate_IdAn");
            AddColumn("dbo.PlanInvatamants", "IdAnScolar", c => c.Int(nullable: false));
            AlterColumn("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", c => c.Int());
            CreateIndex("dbo.PlanInvatamants", "AnDeValabilitate_IdAn");
            AddForeignKey("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", "dbo.AnScolars", "IdAn");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", "dbo.AnScolars");
            DropIndex("dbo.PlanInvatamants", new[] { "AnDeValabilitate_IdAn" });
            AlterColumn("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", c => c.Int(nullable: false));
            DropColumn("dbo.PlanInvatamants", "IdAnScolar");
            RenameColumn(table: "dbo.PlanInvatamants", name: "AnDeValabilitate_IdAn", newName: "IdAn");
            CreateIndex("dbo.PlanInvatamants", "IdAn");
            AddForeignKey("dbo.PlanInvatamants", "IdAn", "dbo.AnScolars", "IdAn", cascadeDelete: true);
        }
    }
}
