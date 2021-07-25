namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateelev : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clasas", "NumeClasa", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clasas", "NumeClasa", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
