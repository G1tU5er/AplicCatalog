namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addprenumeelev : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Elevs", "Prenume", c => c.String(maxLength: 50));
            AlterColumn("dbo.Elevs", "Nume", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Elevs", "Nume", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Elevs", "Prenume");
        }
    }
}
