namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecatalog3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Catalogs", "Nota", c => c.Int());
            AlterColumn("dbo.Catalogs", "NotaTeza", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Catalogs", "NotaTeza", c => c.Int(nullable: false));
            AlterColumn("dbo.Catalogs", "Nota", c => c.Int(nullable: false));
        }
    }
}
