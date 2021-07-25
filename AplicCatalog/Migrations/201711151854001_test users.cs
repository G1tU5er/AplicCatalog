namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testusers : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Parintes", name: "ApplicationUser_Id", newName: "user_Id");
            RenameColumn(table: "dbo.Profesors", name: "ApplicationUser_Id", newName: "user_Id");
            RenameIndex(table: "dbo.Profesors", name: "IX_ApplicationUser_Id", newName: "IX_user_Id");
            RenameIndex(table: "dbo.Parintes", name: "IX_ApplicationUser_Id", newName: "IX_user_Id");
            DropColumn("dbo.Profesors", "OwnerId");
            DropColumn("dbo.Parintes", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parintes", "OwnerId", c => c.String(nullable: false));
            AddColumn("dbo.Profesors", "OwnerId", c => c.String(nullable: false));
            RenameIndex(table: "dbo.Parintes", name: "IX_user_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Profesors", name: "IX_user_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Profesors", name: "user_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Parintes", name: "user_Id", newName: "ApplicationUser_Id");
        }
    }
}
