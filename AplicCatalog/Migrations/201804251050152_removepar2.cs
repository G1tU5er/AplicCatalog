namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removepar2 : DbMigration
    {
        public override void Up()
        {
          
            DropForeignKey("dbo.Parintes", "user_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Elevs", new[] { "IdParinte" });
            DropIndex("dbo.Parintes", new[] { "user_Id" });
            AddColumn("dbo.Elevs", "Mama", c => c.String());
            AddColumn("dbo.Elevs", "Tata", c => c.String());
            DropColumn("dbo.Elevs", "IdParinte");
            DropTable("dbo.Parintes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Parintes",
                c => new
                    {
                        IdParinte = c.Int(nullable: false, identity: true),
                        NumeTata = c.String(maxLength: 50),
                        Telefon = c.String(maxLength: 50),
                        NumeMama = c.String(),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdParinte);
            
            AddColumn("dbo.Elevs", "IdParinte", c => c.Int(nullable: false));
            DropColumn("dbo.Elevs", "Tata");
            DropColumn("dbo.Elevs", "Mama");
            CreateIndex("dbo.Parintes", "user_Id");
            CreateIndex("dbo.Elevs", "IdParinte");
            AddForeignKey("dbo.Parintes", "user_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Elevs", "IdParinte", "dbo.Parintes", "IdParinte");
        }
    }
}
