namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addidmaterie : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropIndex("dbo.PlanInvatamants", new[] { "Materie_IdMaterie" });
            RenameColumn(table: "dbo.PlanInvatamants", name: "Materie_IdMaterie", newName: "IdMaterie");
            AlterColumn("dbo.PlanInvatamants", "IdMaterie", c => c.Int(nullable: false));
            CreateIndex("dbo.PlanInvatamants", "IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "IdMaterie", "dbo.Materies", "IdMaterie", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanInvatamants", "IdMaterie", "dbo.Materies");
            DropIndex("dbo.PlanInvatamants", new[] { "IdMaterie" });
            AlterColumn("dbo.PlanInvatamants", "IdMaterie", c => c.Int());
            RenameColumn(table: "dbo.PlanInvatamants", name: "IdMaterie", newName: "Materie_IdMaterie");
            CreateIndex("dbo.PlanInvatamants", "Materie_IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie");
        }
    }
}
