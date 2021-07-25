namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testuser2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Profesors", "Email");
            DropColumn("dbo.Parintes", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parintes", "Email", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Profesors", "Email", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
