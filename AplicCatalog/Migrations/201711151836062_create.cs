namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catalogs",
                c => new
                    {
                        IdNota = c.Int(nullable: false, identity: true),
                        Nota = c.Int(nullable: false),
                        DataNota = c.DateTime(nullable: false),
                        DataAbsenta = c.DateTime(),
                        AbsentaMotivata = c.String(maxLength: 1),
                        MailTrimis = c.String(maxLength: 1),
                        NotaTeza = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdNota);
            
            CreateTable(
                "dbo.Elevs",
                c => new
                    {
                        IdElev = c.Int(nullable: false, identity: true),
                        Nume = c.String(nullable: false, maxLength: 50),
                        Catalog_IdNota = c.Int(),
                        Parinte_IdParinte = c.Int(),
                    })
                .PrimaryKey(t => t.IdElev)
                .ForeignKey("dbo.Catalogs", t => t.Catalog_IdNota)
                .ForeignKey("dbo.Parintes", t => t.Parinte_IdParinte)
                .Index(t => t.Catalog_IdNota)
                .Index(t => t.Parinte_IdParinte);
            
            CreateTable(
                "dbo.AnScolars",
                c => new
                    {
                        IdAn = c.Int(nullable: false, identity: true),
                        An = c.Int(nullable: false),
                        Elev_IdElev = c.Int(),
                        Profesor_IdProfesor = c.Int(),
                        Orar_IdOrar = c.Int(),
                    })
                .PrimaryKey(t => t.IdAn)
                .ForeignKey("dbo.Elevs", t => t.Elev_IdElev)
                .ForeignKey("dbo.Profesors", t => t.Profesor_IdProfesor)
                .ForeignKey("dbo.Orars", t => t.Orar_IdOrar)
                .Index(t => t.Elev_IdElev)
                .Index(t => t.Profesor_IdProfesor)
                .Index(t => t.Orar_IdOrar);
            
            CreateTable(
                "dbo.Clasas",
                c => new
                    {
                        IdClasa = c.Int(nullable: false, identity: true),
                        NumeClasa = c.String(nullable: false, maxLength: 50),
                        Elev_IdElev = c.Int(),
                        Profesor_IdProfesor = c.Int(),
                        Orar_IdOrar = c.Int(),
                    })
                .PrimaryKey(t => t.IdClasa)
                .ForeignKey("dbo.Elevs", t => t.Elev_IdElev)
                .ForeignKey("dbo.Profesors", t => t.Profesor_IdProfesor)
                .ForeignKey("dbo.Orars", t => t.Orar_IdOrar)
                .Index(t => t.Elev_IdElev)
                .Index(t => t.Profesor_IdProfesor)
                .Index(t => t.Orar_IdOrar);
            
            CreateTable(
                "dbo.Materies",
                c => new
                    {
                        IdMaterie = c.Int(nullable: false, identity: true),
                        NumeMaterie = c.String(nullable: false, maxLength: 50),
                        NrOre = c.Int(nullable: false),
                        NrMaxAbsente = c.Int(nullable: false),
                        Orar_IdOrar = c.Int(),
                    })
                .PrimaryKey(t => t.IdMaterie)
                .ForeignKey("dbo.Orars", t => t.Orar_IdOrar)
                .Index(t => t.Orar_IdOrar);
            
            CreateTable(
                "dbo.Profesors",
                c => new
                    {
                        IdProfesor = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(nullable: false),
                        Nume = c.String(nullable: false, maxLength: 50),
                        TipUtilizator = c.String(nullable: false, maxLength: 10),
                        Telefon = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Orar_IdOrar = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdProfesor)
                .ForeignKey("dbo.Orars", t => t.Orar_IdOrar)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Orar_IdOrar)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Orars",
                c => new
                    {
                        IdOrar = c.Int(nullable: false, identity: true),
                        ZiSaptamana = c.String(nullable: false, maxLength: 10),
                        Ora = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.IdOrar);
            
            CreateTable(
                "dbo.Parintes",
                c => new
                    {
                        IdParinte = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(nullable: false),
                        Nume = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Telefon = c.String(maxLength: 50),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdParinte)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Scoala = c.String(nullable: false, maxLength: 50),
                        Permisiune = c.String(nullable: false, maxLength: 10),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MaterieCatalogs",
                c => new
                    {
                        Materie_IdMaterie = c.Int(nullable: false),
                        Catalog_IdNota = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Materie_IdMaterie, t.Catalog_IdNota })
                .ForeignKey("dbo.Materies", t => t.Materie_IdMaterie, cascadeDelete: true)
                .ForeignKey("dbo.Catalogs", t => t.Catalog_IdNota, cascadeDelete: true)
                .Index(t => t.Materie_IdMaterie)
                .Index(t => t.Catalog_IdNota);
            
            CreateTable(
                "dbo.ProfesorMateries",
                c => new
                    {
                        Profesor_IdProfesor = c.Int(nullable: false),
                        Materie_IdMaterie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profesor_IdProfesor, t.Materie_IdMaterie })
                .ForeignKey("dbo.Profesors", t => t.Profesor_IdProfesor, cascadeDelete: true)
                .ForeignKey("dbo.Materies", t => t.Materie_IdMaterie, cascadeDelete: true)
                .Index(t => t.Profesor_IdProfesor)
                .Index(t => t.Materie_IdMaterie);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Profesors", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Parintes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Elevs", "Parinte_IdParinte", "dbo.Parintes");
            DropForeignKey("dbo.Profesors", "Orar_IdOrar", "dbo.Orars");
            DropForeignKey("dbo.Materies", "Orar_IdOrar", "dbo.Orars");
            DropForeignKey("dbo.Clasas", "Orar_IdOrar", "dbo.Orars");
            DropForeignKey("dbo.AnScolars", "Orar_IdOrar", "dbo.Orars");
            DropForeignKey("dbo.ProfesorMateries", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.ProfesorMateries", "Profesor_IdProfesor", "dbo.Profesors");
            DropForeignKey("dbo.Clasas", "Profesor_IdProfesor", "dbo.Profesors");
            DropForeignKey("dbo.AnScolars", "Profesor_IdProfesor", "dbo.Profesors");
            DropForeignKey("dbo.MaterieCatalogs", "Catalog_IdNota", "dbo.Catalogs");
            DropForeignKey("dbo.MaterieCatalogs", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.Elevs", "Catalog_IdNota", "dbo.Catalogs");
            DropForeignKey("dbo.Clasas", "Elev_IdElev", "dbo.Elevs");
            DropForeignKey("dbo.AnScolars", "Elev_IdElev", "dbo.Elevs");
            DropIndex("dbo.ProfesorMateries", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.ProfesorMateries", new[] { "Profesor_IdProfesor" });
            DropIndex("dbo.MaterieCatalogs", new[] { "Catalog_IdNota" });
            DropIndex("dbo.MaterieCatalogs", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Parintes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Profesors", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Profesors", new[] { "Orar_IdOrar" });
            DropIndex("dbo.Materies", new[] { "Orar_IdOrar" });
            DropIndex("dbo.Clasas", new[] { "Orar_IdOrar" });
            DropIndex("dbo.Clasas", new[] { "Profesor_IdProfesor" });
            DropIndex("dbo.Clasas", new[] { "Elev_IdElev" });
            DropIndex("dbo.AnScolars", new[] { "Orar_IdOrar" });
            DropIndex("dbo.AnScolars", new[] { "Profesor_IdProfesor" });
            DropIndex("dbo.AnScolars", new[] { "Elev_IdElev" });
            DropIndex("dbo.Elevs", new[] { "Parinte_IdParinte" });
            DropIndex("dbo.Elevs", new[] { "Catalog_IdNota" });
            DropTable("dbo.ProfesorMateries");
            DropTable("dbo.MaterieCatalogs");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Parintes");
            DropTable("dbo.Orars");
            DropTable("dbo.Profesors");
            DropTable("dbo.Materies");
            DropTable("dbo.Clasas");
            DropTable("dbo.AnScolars");
            DropTable("dbo.Elevs");
            DropTable("dbo.Catalogs");
        }
    }
}
