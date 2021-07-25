namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecatalog : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Catalogs", "DataNota", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Catalogs", "DataNota", c => c.DateTime(nullable: false));
        }
    }
}
