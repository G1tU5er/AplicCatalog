namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addincadrare2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TipUtilizatorProfesors", "TipUtilizator_IdTipUtilizator", "dbo.TipUtilizators");
            DropForeignKey("dbo.TipUtilizatorProfesors", "Profesor_IdProfesor", "dbo.Profesors");
            DropIndex("dbo.TipUtilizatorProfesors", new[] { "TipUtilizator_IdTipUtilizator" });
            DropIndex("dbo.TipUtilizatorProfesors", new[] { "Profesor_IdProfesor" });
            CreateTable(
                "dbo.Incadrares",
                c => new
                    {
                        IdIncadrare = c.Int(nullable: false),
                        Clasa_IdClasa = c.Int(),
                    })
                .PrimaryKey(t => t.IdIncadrare)
                .ForeignKey("dbo.Clasas", t => t.Clasa_IdClasa)
                .ForeignKey("dbo.Elevs", t => t.IdIncadrare)
                .Index(t => t.IdIncadrare)
                .Index(t => t.Clasa_IdClasa);
            
            DropColumn("dbo.Parintes", "OwnerId");
            DropTable("dbo.TipUtilizators");
            DropTable("dbo.TipUtilizatorProfesors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TipUtilizatorProfesors",
                c => new
                    {
                        TipUtilizator_IdTipUtilizator = c.Int(nullable: false),
                        Profesor_IdProfesor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TipUtilizator_IdTipUtilizator, t.Profesor_IdProfesor });
            
            CreateTable(
                "dbo.TipUtilizators",
                c => new
                    {
                        IdTipUtilizator = c.Int(nullable: false, identity: true),
                        NumeTipUtilizator = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdTipUtilizator);
            
            AddColumn("dbo.Parintes", "OwnerId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Incadrares", "IdIncadrare", "dbo.Elevs");
            DropForeignKey("dbo.Incadrares", "Clasa_IdClasa", "dbo.Clasas");
            DropIndex("dbo.Incadrares", new[] { "Clasa_IdClasa" });
            DropIndex("dbo.Incadrares", new[] { "IdIncadrare" });
            DropTable("dbo.Incadrares");
            CreateIndex("dbo.TipUtilizatorProfesors", "Profesor_IdProfesor");
            CreateIndex("dbo.TipUtilizatorProfesors", "TipUtilizator_IdTipUtilizator");
            AddForeignKey("dbo.TipUtilizatorProfesors", "Profesor_IdProfesor", "dbo.Profesors", "IdProfesor");
            AddForeignKey("dbo.TipUtilizatorProfesors", "TipUtilizator_IdTipUtilizator", "dbo.TipUtilizators", "IdTipUtilizator");
        }
    }
}
