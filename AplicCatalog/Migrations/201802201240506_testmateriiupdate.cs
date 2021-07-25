namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testmateriiupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropIndex("dbo.PlanInvatamants", new[] { "Materie_IdMaterie" });
            CreateTable(
                "dbo.PlanInvatamantMateries",
                c => new
                    {
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                        Materie_IdMaterie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanInvatamant_IdPlan, t.Materie_IdMaterie })
                .ForeignKey("dbo.PlanInvatamants", t => t.PlanInvatamant_IdPlan, cascadeDelete: true)
                .ForeignKey("dbo.Materies", t => t.Materie_IdMaterie, cascadeDelete: true)
                .Index(t => t.PlanInvatamant_IdPlan)
                .Index(t => t.Materie_IdMaterie);
            
            AddColumn("dbo.Materies", "NrOre", c => c.Int(nullable: false));
            DropColumn("dbo.PlanInvatamants", "NrOre");
            DropColumn("dbo.PlanInvatamants", "Materie_IdMaterie");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanInvatamants", "Materie_IdMaterie", c => c.Int());
            AddColumn("dbo.PlanInvatamants", "NrOre", c => c.Int(nullable: false));
            DropForeignKey("dbo.PlanInvatamantMateries", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.PlanInvatamantMateries", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropIndex("dbo.PlanInvatamantMateries", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.PlanInvatamantMateries", new[] { "PlanInvatamant_IdPlan" });
            DropColumn("dbo.Materies", "NrOre");
            DropTable("dbo.PlanInvatamantMateries");
            CreateIndex("dbo.PlanInvatamants", "Materie_IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie");
        }
    }
}
