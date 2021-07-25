namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addplaninvatamantupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanInvatamants", "Clasa_IdClasa", c => c.Int());
            AddColumn("dbo.PlanInvatamants", "Materie_IdMaterie", c => c.Int());
            CreateIndex("dbo.PlanInvatamants", "Clasa_IdClasa");
            CreateIndex("dbo.PlanInvatamants", "Materie_IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "Clasa_IdClasa", "dbo.Clasas", "IdClasa");
            AddForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie");
            DropColumn("dbo.Materies", "NrOre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materies", "NrOre", c => c.Int(nullable: false));
            DropForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.PlanInvatamants", "Clasa_IdClasa", "dbo.Clasas");
            DropIndex("dbo.PlanInvatamants", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.PlanInvatamants", new[] { "Clasa_IdClasa" });
            DropColumn("dbo.PlanInvatamants", "Materie_IdMaterie");
            DropColumn("dbo.PlanInvatamants", "Clasa_IdClasa");
        }
    }
}
