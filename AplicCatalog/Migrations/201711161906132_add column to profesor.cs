namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumntoprofesor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profesors", "OwnerId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profesors", "OwnerId");
        }
    }
}
