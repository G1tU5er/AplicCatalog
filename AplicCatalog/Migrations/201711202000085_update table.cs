namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Profesors", "Telefon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profesors", "Telefon", c => c.String(maxLength: 50));
        }
    }
}
