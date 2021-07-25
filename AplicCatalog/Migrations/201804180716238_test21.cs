namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Profesors", "AnScolar_IdAn", "dbo.AnScolars");
            DropForeignKey("dbo.Clasas", "Profesor_IdProfesor", "dbo.Profesors");
            DropIndex("dbo.Clasas", new[] { "Profesor_IdProfesor" });
            DropIndex("dbo.Profesors", new[] { "AnScolar_IdAn" });
            AddColumn("dbo.Elevs", "Mama_IdParinte", c => c.Int());
            AddColumn("dbo.Elevs", "Tata_IdParinte", c => c.Int());
            CreateIndex("dbo.Elevs", "Mama_IdParinte");
            CreateIndex("dbo.Elevs", "Tata_IdParinte");
            AddForeignKey("dbo.Elevs", "Mama_IdParinte", "dbo.Parintes", "IdParinte");
            AddForeignKey("dbo.Elevs", "Tata_IdParinte", "dbo.Parintes", "IdParinte");
            DropColumn("dbo.Clasas", "Profesor_IdProfesor");
            DropColumn("dbo.Profesors", "OwnerId");
            DropColumn("dbo.Profesors", "AnScolar_IdAn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profesors", "AnScolar_IdAn", c => c.Int());
            AddColumn("dbo.Profesors", "OwnerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Clasas", "Profesor_IdProfesor", c => c.Int());
            DropForeignKey("dbo.Elevs", "Tata_IdParinte", "dbo.Parintes");
            DropForeignKey("dbo.Elevs", "Mama_IdParinte", "dbo.Parintes");
            DropIndex("dbo.Elevs", new[] { "Tata_IdParinte" });
            DropIndex("dbo.Elevs", new[] { "Mama_IdParinte" });
            DropColumn("dbo.Elevs", "Tata_IdParinte");
            DropColumn("dbo.Elevs", "Mama_IdParinte");
            CreateIndex("dbo.Profesors", "AnScolar_IdAn");
            CreateIndex("dbo.Clasas", "Profesor_IdProfesor");
            AddForeignKey("dbo.Clasas", "Profesor_IdProfesor", "dbo.Profesors", "IdProfesor");
            AddForeignKey("dbo.Profesors", "AnScolar_IdAn", "dbo.AnScolars", "IdAn");
        }
    }
}
