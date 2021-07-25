namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addincadrareprof : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncadrareProfs",
                c => new
                    {
                        IdxIncadrare = c.Int(nullable: false, identity: true),
                        IdProfesor = c.Int(nullable: false),
                        idClasa = c.Int(nullable: false),
                        idAnScolar = c.Int(nullable: false),
                        idMaterie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdxIncadrare)
                .ForeignKey("dbo.Clasas", t => t.idClasa)
                .ForeignKey("dbo.Materies", t => t.idMaterie)
                .ForeignKey("dbo.Profesors", t => t.IdProfesor)
                .Index(t => t.IdProfesor)
                .Index(t => t.idClasa)
                .Index(t => t.idMaterie);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncadrareProfs", "IdProfesor", "dbo.Profesors");
            DropForeignKey("dbo.IncadrareProfs", "idMaterie", "dbo.Materies");
            DropForeignKey("dbo.IncadrareProfs", "idClasa", "dbo.Clasas");
            DropIndex("dbo.IncadrareProfs", new[] { "idMaterie" });
            DropIndex("dbo.IncadrareProfs", new[] { "idClasa" });
            DropIndex("dbo.IncadrareProfs", new[] { "IdProfesor" });
            DropTable("dbo.IncadrareProfs");
        }
    }
}
