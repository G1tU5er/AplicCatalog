namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setlinktomatandprof : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Materies", "Profesor_IdProfesor", "dbo.Profesors");
            DropIndex("dbo.Materies", new[] { "Profesor_IdProfesor" });
            CreateTable(
                "dbo.ProfesorMateries",
                c => new
                    {
                        Profesor_IdProfesor = c.Int(nullable: false),
                        Materie_IdMaterie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profesor_IdProfesor, t.Materie_IdMaterie })
                .ForeignKey("dbo.Profesors", t => t.Profesor_IdProfesor)
                .ForeignKey("dbo.Materies", t => t.Materie_IdMaterie)
                .Index(t => t.Profesor_IdProfesor)
                .Index(t => t.Materie_IdMaterie);
            
            DropColumn("dbo.Materies", "Profesor_IdProfesor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materies", "Profesor_IdProfesor", c => c.Int());
            DropForeignKey("dbo.ProfesorMateries", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.ProfesorMateries", "Profesor_IdProfesor", "dbo.Profesors");
            DropIndex("dbo.ProfesorMateries", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.ProfesorMateries", new[] { "Profesor_IdProfesor" });
            DropTable("dbo.ProfesorMateries");
            CreateIndex("dbo.Materies", "Profesor_IdProfesor");
            AddForeignKey("dbo.Materies", "Profesor_IdProfesor", "dbo.Profesors", "IdProfesor");
        }
    }
}
