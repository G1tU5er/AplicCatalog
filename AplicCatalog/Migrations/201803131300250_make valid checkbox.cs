namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makevalidcheckbox : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Valid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Valid", c => c.String(maxLength: 1));
        }
    }
}
