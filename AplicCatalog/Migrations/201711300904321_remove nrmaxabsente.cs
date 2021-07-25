namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removenrmaxabsente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clasas", "NrMaxAbsente", c => c.Int(nullable: false));
            DropColumn("dbo.Materies", "NrMaxAbsente");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materies", "NrMaxAbsente", c => c.Int(nullable: false));
            DropColumn("dbo.Clasas", "NrMaxAbsente");
        }
    }
}
