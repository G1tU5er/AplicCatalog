namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateidan : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlanInvatamants", "IdAnScolar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanInvatamants", "IdAnScolar", c => c.Int(nullable: false));
        }
    }
}
