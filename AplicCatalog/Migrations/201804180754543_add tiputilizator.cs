namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtiputilizator : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipUtilizators",
                c => new
                    {
                        IdTipUtilizator = c.Int(nullable: false, identity: true),
                        NumeTipUtilizator = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdTipUtilizator);
            
            CreateTable(
                "dbo.TipUtilizatorProfesors",
                c => new
                    {
                        TipUtilizator_IdTipUtilizator = c.Int(nullable: false),
                        Profesor_IdProfesor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TipUtilizator_IdTipUtilizator, t.Profesor_IdProfesor })
                .ForeignKey("dbo.TipUtilizators", t => t.TipUtilizator_IdTipUtilizator)
                .ForeignKey("dbo.Profesors", t => t.Profesor_IdProfesor)
                .Index(t => t.TipUtilizator_IdTipUtilizator)
                .Index(t => t.Profesor_IdProfesor);
            
            DropColumn("dbo.Profesors", "TipUtilizator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profesors", "TipUtilizator", c => c.String(nullable: false, maxLength: 10));
            DropForeignKey("dbo.TipUtilizatorProfesors", "Profesor_IdProfesor", "dbo.Profesors");
            DropForeignKey("dbo.TipUtilizatorProfesors", "TipUtilizator_IdTipUtilizator", "dbo.TipUtilizators");
            DropIndex("dbo.TipUtilizatorProfesors", new[] { "Profesor_IdProfesor" });
            DropIndex("dbo.TipUtilizatorProfesors", new[] { "TipUtilizator_IdTipUtilizator" });
            DropTable("dbo.TipUtilizatorProfesors");
            DropTable("dbo.TipUtilizators");
        }
    }
}
