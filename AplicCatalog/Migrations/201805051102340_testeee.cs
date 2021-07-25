namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testeee : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Catalogs", "MailTrimis");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Catalogs", "MailTrimis", c => c.String(maxLength: 1));
        }
    }
}
