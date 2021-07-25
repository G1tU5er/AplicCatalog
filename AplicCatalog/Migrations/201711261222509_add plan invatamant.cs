namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addplaninvatamant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanInvatamants",
                c => new
                    {
                        IdPlan = c.Int(nullable: false, identity: true),
                        NrOre = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPlan);
            
            AddColumn("dbo.AspNetUsers", "DataActivareCont", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "DataExpirareCont", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DataExpirareCont");
            DropColumn("dbo.AspNetUsers", "DataActivareCont");
            DropTable("dbo.PlanInvatamants");
        }
    }
}
