namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disablecascade3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanInvatamants", "IdAn", "dbo.AnScolars");
            DropForeignKey("dbo.Clasas", "IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            AddForeignKey("dbo.PlanInvatamants", "IdAn", "dbo.AnScolars", "IdAn");
            AddForeignKey("dbo.Clasas", "IdPlan", "dbo.PlanInvatamants", "IdPlan");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Clasas", "IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.PlanInvatamants", "IdAn", "dbo.AnScolars");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Clasas", "IdPlan", "dbo.PlanInvatamants", "IdPlan", cascadeDelete: true);
            AddForeignKey("dbo.PlanInvatamants", "IdAn", "dbo.AnScolars", "IdAn", cascadeDelete: true);
        }
    }
}
