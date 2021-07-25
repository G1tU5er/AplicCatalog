namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removetata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Elevs", "Mama_IdParinte", "dbo.Parintes");
            DropForeignKey("dbo.Elevs", "Tata_IdParinte", "dbo.Parintes");
            DropIndex("dbo.Elevs", new[] { "Parinte_IdParinte" });
            DropIndex("dbo.Elevs", new[] { "Mama_IdParinte" });
            DropIndex("dbo.Elevs", new[] { "Tata_IdParinte" });
            RenameColumn(table: "dbo.Elevs", name: "Parinte_IdParinte", newName: "IdParinte");
            AddColumn("dbo.Parintes", "NumeTata", c => c.String(maxLength: 50));
            AddColumn("dbo.Parintes", "NumeMama", c => c.String());
            AlterColumn("dbo.Elevs", "IdParinte", c => c.Int(nullable: false));
            CreateIndex("dbo.Elevs", "IdParinte");
            DropColumn("dbo.Elevs", "Mama_IdParinte");
            DropColumn("dbo.Elevs", "Tata_IdParinte");
            DropColumn("dbo.Parintes", "Nume");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parintes", "Nume", c => c.String(maxLength: 50));
            AddColumn("dbo.Elevs", "Tata_IdParinte", c => c.Int());
            AddColumn("dbo.Elevs", "Mama_IdParinte", c => c.Int());
            DropIndex("dbo.Elevs", new[] { "IdParinte" });
            AlterColumn("dbo.Elevs", "IdParinte", c => c.Int());
            DropColumn("dbo.Parintes", "NumeMama");
            DropColumn("dbo.Parintes", "NumeTata");
            RenameColumn(table: "dbo.Elevs", name: "IdParinte", newName: "Parinte_IdParinte");
            CreateIndex("dbo.Elevs", "Tata_IdParinte");
            CreateIndex("dbo.Elevs", "Mama_IdParinte");
            CreateIndex("dbo.Elevs", "Parinte_IdParinte");
            AddForeignKey("dbo.Elevs", "Tata_IdParinte", "dbo.Parintes", "IdParinte");
            AddForeignKey("dbo.Elevs", "Mama_IdParinte", "dbo.Parintes", "IdParinte");
        }
    }
}
