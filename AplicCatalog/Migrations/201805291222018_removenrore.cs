namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removenrore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncadrareMateriePlans",
                c => new
                    {
                        IdincadrareMat = c.Int(nullable: false, identity: true),
                        NrOre = c.Int(nullable: false),
                        IdMaterie = c.Int(nullable: false),
                        idPlanInv = c.Int(nullable: false),
                        PlanInvatamant_IdPlan = c.Int(),
                    })
                .PrimaryKey(t => t.IdincadrareMat)
                .ForeignKey("dbo.Materies", t => t.IdMaterie)
                .ForeignKey("dbo.PlanInvatamants", t => t.PlanInvatamant_IdPlan)
                .Index(t => t.IdMaterie)
                .Index(t => t.PlanInvatamant_IdPlan);
            
            DropColumn("dbo.Materies", "NrOre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materies", "NrOre", c => c.Int(nullable: false));
            DropForeignKey("dbo.IncadrareMateriePlans", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.IncadrareMateriePlans", "IdMaterie", "dbo.Materies");
            DropIndex("dbo.IncadrareMateriePlans", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.IncadrareMateriePlans", new[] { "IdMaterie" });
            DropTable("dbo.IncadrareMateriePlans");
        }
    }
}
