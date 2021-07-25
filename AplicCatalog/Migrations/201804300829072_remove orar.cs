namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeorar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnScolars", "Orar_IdOrar", "dbo.Orars");
            DropForeignKey("dbo.Clasas", "Orar_IdOrar", "dbo.Orars");
            DropForeignKey("dbo.Materies", "Orar_IdOrar", "dbo.Orars");
            DropForeignKey("dbo.Profesors", "Orar_IdOrar", "dbo.Orars");
            DropIndex("dbo.AnScolars", new[] { "Orar_IdOrar" });
            DropIndex("dbo.Materies", new[] { "Orar_IdOrar" });
            DropIndex("dbo.Clasas", new[] { "Orar_IdOrar" });
            DropIndex("dbo.Profesors", new[] { "Orar_IdOrar" });
            DropColumn("dbo.AnScolars", "Orar_IdOrar");
            DropColumn("dbo.Materies", "Orar_IdOrar");
            DropColumn("dbo.Clasas", "Orar_IdOrar");
            DropColumn("dbo.Profesors", "Orar_IdOrar");
            DropTable("dbo.Orars");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Orars",
                c => new
                    {
                        IdOrar = c.Int(nullable: false, identity: true),
                        ZiSaptamana = c.String(nullable: false, maxLength: 10),
                        Ora = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.IdOrar);
            
            AddColumn("dbo.Profesors", "Orar_IdOrar", c => c.Int());
            AddColumn("dbo.Clasas", "Orar_IdOrar", c => c.Int());
            AddColumn("dbo.Materies", "Orar_IdOrar", c => c.Int());
            AddColumn("dbo.AnScolars", "Orar_IdOrar", c => c.Int());
            CreateIndex("dbo.Profesors", "Orar_IdOrar");
            CreateIndex("dbo.Clasas", "Orar_IdOrar");
            CreateIndex("dbo.Materies", "Orar_IdOrar");
            CreateIndex("dbo.AnScolars", "Orar_IdOrar");
            AddForeignKey("dbo.Profesors", "Orar_IdOrar", "dbo.Orars", "IdOrar");
            AddForeignKey("dbo.Materies", "Orar_IdOrar", "dbo.Orars", "IdOrar");
            AddForeignKey("dbo.Clasas", "Orar_IdOrar", "dbo.Orars", "IdOrar");
            AddForeignKey("dbo.AnScolars", "Orar_IdOrar", "dbo.Orars", "IdOrar");
        }
    }
}
