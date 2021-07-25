namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removescoala : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Valid", c => c.String(maxLength: 1));
            DropColumn("dbo.AspNetUsers", "Scoala");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Scoala", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.AspNetUsers", "Valid");
        }
    }
}
