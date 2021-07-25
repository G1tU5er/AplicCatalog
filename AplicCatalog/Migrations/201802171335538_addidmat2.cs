namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addidmat2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanInvatamants", "IdMaterie", "dbo.Materies");
            DropIndex("dbo.PlanInvatamants", new[] { "IdMaterie" });
            RenameColumn(table: "dbo.PlanInvatamants", name: "IdMaterie", newName: "Materie_IdMaterie");
            AlterColumn("dbo.PlanInvatamants", "Materie_IdMaterie", c => c.Int());
            CreateIndex("dbo.PlanInvatamants", "Materie_IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropIndex("dbo.PlanInvatamants", new[] { "Materie_IdMaterie" });
            AlterColumn("dbo.PlanInvatamants", "Materie_IdMaterie", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PlanInvatamants", name: "Materie_IdMaterie", newName: "IdMaterie");
            CreateIndex("dbo.PlanInvatamants", "IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "IdMaterie", "dbo.Materies", "IdMaterie", cascadeDelete: true);
        }
    }
}
