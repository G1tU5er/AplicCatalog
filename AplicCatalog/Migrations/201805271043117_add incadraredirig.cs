namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addincadraredirig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncadrareDirigintes",
                c => new
                    {
                        IdxIncadrare = c.Int(nullable: false, identity: true),
                        IdProfesor = c.Int(nullable: false),
                        idClasa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdxIncadrare)
                .ForeignKey("dbo.Clasas", t => t.idClasa)
                .ForeignKey("dbo.Profesors", t => t.IdProfesor)
                .Index(t => t.IdProfesor)
                .Index(t => t.idClasa);
            
            DropColumn("dbo.IncadrareProfs", "EDiriginte");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IncadrareProfs", "EDiriginte", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.IncadrareDirigintes", "IdProfesor", "dbo.Profesors");
            DropForeignKey("dbo.IncadrareDirigintes", "idClasa", "dbo.Clasas");
            DropIndex("dbo.IncadrareDirigintes", new[] { "idClasa" });
            DropIndex("dbo.IncadrareDirigintes", new[] { "IdProfesor" });
            DropTable("dbo.IncadrareDirigintes");
        }
    }
}
