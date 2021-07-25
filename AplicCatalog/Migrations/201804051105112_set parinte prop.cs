namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setparinteprop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnScolars", "Profesor_IdProfesor", "dbo.Profesors");
            DropIndex("dbo.AnScolars", new[] { "Profesor_IdProfesor" });
            AddColumn("dbo.Profesors", "AnScolar_IdAn", c => c.Int());
            AddColumn("dbo.Parintes", "OwnerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Parintes", "Nume", c => c.String(maxLength: 50));
            CreateIndex("dbo.Profesors", "AnScolar_IdAn");
            AddForeignKey("dbo.Profesors", "AnScolar_IdAn", "dbo.AnScolars", "IdAn");
            DropColumn("dbo.AnScolars", "Profesor_IdProfesor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AnScolars", "Profesor_IdProfesor", c => c.Int());
            DropForeignKey("dbo.Profesors", "AnScolar_IdAn", "dbo.AnScolars");
            DropIndex("dbo.Profesors", new[] { "AnScolar_IdAn" });
            AlterColumn("dbo.Parintes", "Nume", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Parintes", "OwnerId");
            DropColumn("dbo.Profesors", "AnScolar_IdAn");
            CreateIndex("dbo.AnScolars", "Profesor_IdProfesor");
            AddForeignKey("dbo.AnScolars", "Profesor_IdProfesor", "dbo.Profesors", "IdProfesor");
        }
    }
}
