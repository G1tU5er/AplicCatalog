namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Elevs", "IdParinte_M", c => c.Int(nullable: false));
            AddColumn("dbo.Elevs", "IdParinte_T", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Elevs", "IdParinte_T");
            DropColumn("dbo.Elevs", "IdParinte_M");
        }
    }
}
