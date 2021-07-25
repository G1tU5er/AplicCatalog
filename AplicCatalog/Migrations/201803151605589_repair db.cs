namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repairdb : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MateriePlanInvatamants", newName: "PlanInvatamantMateries");
            DropForeignKey("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", "dbo.AnScolars");
            DropIndex("dbo.PlanInvatamants", new[] { "AnDeValabilitate_IdAn" });
            RenameColumn(table: "dbo.PlanInvatamants", name: "AnDeValabilitate_IdAn", newName: "IdAn");
            DropPrimaryKey("dbo.PlanInvatamantMateries");
            AlterColumn("dbo.PlanInvatamants", "IdAn", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Permisiune", c => c.String(maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "Valid", c => c.Boolean(nullable: false));
            AddPrimaryKey("dbo.PlanInvatamantMateries", new[] { "PlanInvatamant_IdPlan", "Materie_IdMaterie" });
            CreateIndex("dbo.PlanInvatamants", "IdAn");
            AddForeignKey("dbo.PlanInvatamants", "IdAn", "dbo.AnScolars", "IdAn", cascadeDelete: true);
            DropColumn("dbo.PlanInvatamants", "IdAnScolar");
            DropTable("dbo.AnScolar1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AnScolar1",
                c => new
                    {
                        IdAnScolar = c.Int(nullable: false, identity: true),
                        InceputAnScolar1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAnScolar);
            
            AddColumn("dbo.PlanInvatamants", "IdAnScolar", c => c.Int(nullable: false));
            DropForeignKey("dbo.PlanInvatamants", "IdAn", "dbo.AnScolars");
            DropIndex("dbo.PlanInvatamants", new[] { "IdAn" });
            DropPrimaryKey("dbo.PlanInvatamantMateries");
            AlterColumn("dbo.AspNetUsers", "Valid", c => c.String(maxLength: 1));
            AlterColumn("dbo.AspNetUsers", "Permisiune", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.PlanInvatamants", "IdAn", c => c.Int());
            AddPrimaryKey("dbo.PlanInvatamantMateries", new[] { "Materie_IdMaterie", "PlanInvatamant_IdPlan" });
            RenameColumn(table: "dbo.PlanInvatamants", name: "IdAn", newName: "AnDeValabilitate_IdAn");
            CreateIndex("dbo.PlanInvatamants", "AnDeValabilitate_IdAn");
            AddForeignKey("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", "dbo.AnScolars", "IdAn");
            RenameTable(name: "dbo.PlanInvatamantMateries", newName: "MateriePlanInvatamants");
        }
    }
}
