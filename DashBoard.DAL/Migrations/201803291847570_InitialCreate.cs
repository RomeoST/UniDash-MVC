namespace DashBoard.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAction",
                c => new
                    {
                        ActionId = c.Int(nullable: false, identity: true),
                        ControllerId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ActionId)
                .ForeignKey("dbo.TController", t => t.ControllerId)
                .Index(t => t.ControllerId);
            
            CreateTable(
                "dbo.TController",
                c => new
                    {
                        ControllerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ControllerId);
            
            CreateTable(
                "dbo.Applicant",
                c => new
                    {
                        ApplicantId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        NameFound = c.String(),
                        NameApplicant = c.String(),
                        MailApplicant = c.String(),
                        PhoneApplicant = c.String(),
                        SchoolCollege = c.String(),
                        Address = c.String(),
                        Speciality = c.String(),
                        MarkResult = c.String(),
                        DateEdit = c.DateTime(nullable: false),
                        DateAdd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicantId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                "dbo.ClientProfile",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Enable = c.Boolean(nullable: false),
                        FullName = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastVisitDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        FacultyId = c.Int(),
                        InstituteId = c.Int(),
                        isAdmission = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculty", t => t.FacultyId)
                .ForeignKey("dbo.Institute", t => t.InstituteId)
                .Index(t => t.FacultyId)
                .Index(t => t.InstituteId);
            
            CreateTable(
                "dbo.Faculty",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Institute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionRoles",
                c => new
                    {
                        PermissionId = c.Int(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        ControllerId = c.Int(),
                        ActionId = c.Int(),
                    })
                .PrimaryKey(t => t.PermissionId)
                .ForeignKey("dbo.TAction", t => t.ActionId)
                .ForeignKey("dbo.TController", t => t.ControllerId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.ControllerId)
                .Index(t => t.ActionId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SubmissionDoc",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        FullName = c.String(),
                        Zno1 = c.Int(nullable: false),
                        Zno2 = c.Int(nullable: false),
                        Zno3 = c.Int(nullable: false),
                        Sertificate = c.Int(nullable: false),
                        Ball = c.Int(nullable: false),
                        Privilege = c.Boolean(nullable: false),
                        Speciality = c.String(),
                        Sum = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubmissionDoc", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PermissionRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PermissionRoles", "ControllerId", "dbo.TController");
            DropForeignKey("dbo.PermissionRoles", "ActionId", "dbo.TAction");
            DropForeignKey("dbo.Department", "InstituteId", "dbo.Institute");
            DropForeignKey("dbo.Department", "FacultyId", "dbo.Faculty");
            DropForeignKey("dbo.Applicant", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClientProfile", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TAction", "ControllerId", "dbo.TController");
            DropIndex("dbo.SubmissionDoc", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PermissionRoles", new[] { "ActionId" });
            DropIndex("dbo.PermissionRoles", new[] { "ControllerId" });
            DropIndex("dbo.PermissionRoles", new[] { "RoleId" });
            DropIndex("dbo.Department", new[] { "InstituteId" });
            DropIndex("dbo.Department", new[] { "FacultyId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.ClientProfile", new[] { "Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Applicant", new[] { "UserId" });
            DropIndex("dbo.TAction", new[] { "ControllerId" });
            DropTable("dbo.SubmissionDoc");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PermissionRoles");
            DropTable("dbo.Institute");
            DropTable("dbo.Faculty");
            DropTable("dbo.Department");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.ClientProfile");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Applicant");
            DropTable("dbo.TController");
            DropTable("dbo.TAction");
        }
    }
}
