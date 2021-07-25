namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removerequiredpermisiune : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Permisiune", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Permisiune", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
