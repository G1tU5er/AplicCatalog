namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropdownlistsave : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.PlanInvatamantClasas", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.PlanInvatamantClasas", "Clasa_IdClasa", "dbo.Clasas");
            DropForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropIndex("dbo.AnScolars", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.PlanInvatamantClasas", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.PlanInvatamantClasas", new[] { "Clasa_IdClasa" });
            DropIndex("dbo.MateriePlanInvatamants", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.MateriePlanInvatamants", new[] { "PlanInvatamant_IdPlan" });
            AddColumn("dbo.PlanInvatamants", "Clasa_IdClasa", c => c.Int());
            AddColumn("dbo.PlanInvatamants", "Materie_IdMaterie", c => c.Int());
            CreateIndex("dbo.PlanInvatamants", "Clasa_IdClasa");
            CreateIndex("dbo.PlanInvatamants", "Materie_IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "Clasa_IdClasa", "dbo.Clasas", "IdClasa");
            AddForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie");
            DropColumn("dbo.AnScolars", "PlanInvatamant_IdPlan");
            DropTable("dbo.PlanInvatamantClasas");
            DropTable("dbo.MateriePlanInvatamants");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MateriePlanInvatamants",
                c => new
                    {
                        Materie_IdMaterie = c.Int(nullable: false),
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Materie_IdMaterie, t.PlanInvatamant_IdPlan });
            
            CreateTable(
                "dbo.PlanInvatamantClasas",
                c => new
                    {
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                        Clasa_IdClasa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanInvatamant_IdPlan, t.Clasa_IdClasa });
            
            AddColumn("dbo.AnScolars", "PlanInvatamant_IdPlan", c => c.Int());
            DropForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.PlanInvatamants", "Clasa_IdClasa", "dbo.Clasas");
            DropIndex("dbo.PlanInvatamants", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.PlanInvatamants", new[] { "Clasa_IdClasa" });
            DropColumn("dbo.PlanInvatamants", "Materie_IdMaterie");
            DropColumn("dbo.PlanInvatamants", "Clasa_IdClasa");
            CreateIndex("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan");
            CreateIndex("dbo.MateriePlanInvatamants", "Materie_IdMaterie");
            CreateIndex("dbo.PlanInvatamantClasas", "Clasa_IdClasa");
            CreateIndex("dbo.PlanInvatamantClasas", "PlanInvatamant_IdPlan");
            CreateIndex("dbo.AnScolars", "PlanInvatamant_IdPlan");
            AddForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan", cascadeDelete: true);
            AddForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie", cascadeDelete: true);
            AddForeignKey("dbo.PlanInvatamantClasas", "Clasa_IdClasa", "dbo.Clasas", "IdClasa", cascadeDelete: true);
            AddForeignKey("dbo.PlanInvatamantClasas", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan", cascadeDelete: true);
            AddForeignKey("dbo.AnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan");
        }
    }
}
