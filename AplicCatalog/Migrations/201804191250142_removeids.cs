namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeids : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Elevs", "IdParinte_M");
            DropColumn("dbo.Elevs", "IdParinte_T");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Elevs", "IdParinte_T", c => c.Int(nullable: false));
            AddColumn("dbo.Elevs", "IdParinte_M", c => c.Int(nullable: false));
        }
    }
}
