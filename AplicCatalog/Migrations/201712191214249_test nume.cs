namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testnume : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlanInvatamants", "Nume", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlanInvatamants", "Nume", c => c.String(nullable: false));
        }
    }
}
