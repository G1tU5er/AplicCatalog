namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Profesors", "Nume", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Profesors", "Nume", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
