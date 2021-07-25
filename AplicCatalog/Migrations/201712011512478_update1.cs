namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanInvatamants", "An_IdAn", "dbo.AnScolars");
            DropForeignKey("dbo.AnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.PlanInvatamants", "AnScolar_IdAn", "dbo.AnScolars");
            DropIndex("dbo.AnScolars", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.PlanInvatamants", new[] { "An_IdAn" });
            DropIndex("dbo.PlanInvatamants", new[] { "AnScolar_IdAn" });
            CreateTable(
                "dbo.PlanInvatamantAnScolars",
                c => new
                    {
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                        AnScolar_IdAn = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanInvatamant_IdPlan, t.AnScolar_IdAn })
                .ForeignKey("dbo.PlanInvatamants", t => t.PlanInvatamant_IdPlan, cascadeDelete: true)
                .ForeignKey("dbo.AnScolars", t => t.AnScolar_IdAn, cascadeDelete: true)
                .Index(t => t.PlanInvatamant_IdPlan)
                .Index(t => t.AnScolar_IdAn);
            
            DropColumn("dbo.AnScolars", "PlanInvatamant_IdPlan");
            DropColumn("dbo.PlanInvatamants", "An_IdAn");
            DropColumn("dbo.PlanInvatamants", "AnScolar_IdAn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanInvatamants", "AnScolar_IdAn", c => c.Int());
            AddColumn("dbo.PlanInvatamants", "An_IdAn", c => c.Int());
            AddColumn("dbo.AnScolars", "PlanInvatamant_IdPlan", c => c.Int());
            DropForeignKey("dbo.PlanInvatamantAnScolars", "AnScolar_IdAn", "dbo.AnScolars");
            DropForeignKey("dbo.PlanInvatamantAnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropIndex("dbo.PlanInvatamantAnScolars", new[] { "AnScolar_IdAn" });
            DropIndex("dbo.PlanInvatamantAnScolars", new[] { "PlanInvatamant_IdPlan" });
            DropTable("dbo.PlanInvatamantAnScolars");
            CreateIndex("dbo.PlanInvatamants", "AnScolar_IdAn");
            CreateIndex("dbo.PlanInvatamants", "An_IdAn");
            CreateIndex("dbo.AnScolars", "PlanInvatamant_IdPlan");
            AddForeignKey("dbo.PlanInvatamants", "AnScolar_IdAn", "dbo.AnScolars", "IdAn");
            AddForeignKey("dbo.AnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan");
            AddForeignKey("dbo.PlanInvatamants", "An_IdAn", "dbo.AnScolars", "IdAn");
        }
    }
}
