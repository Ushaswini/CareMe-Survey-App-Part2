namespace Homework_04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialMigrationForModels : DbMigration
    {
        public override void Up()
        {
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
                "dbo.StudyGroups",
                c => new
                {
                    StudyGroupId = c.String(nullable: false, maxLength: 128),
                    StudyName = c.String(),
                    StudyCoordinatorId = c.String(nullable: false, maxLength: 128),
                    StudyGroupCreadtedTime = c.String(),
                })
                .PrimaryKey(t => t.StudyGroupId)
                .ForeignKey("dbo.AspNetUsers", t => t.StudyCoordinatorId)
                .Index(t => t.StudyGroupId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    StudyGroupId = c.String(maxLength: 128),
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
                .ForeignKey("dbo.StudyGroups", t => t.StudyGroupId)
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
                "dbo.SurveyResponses",
                c => new
                {
                    SurveyResponseId = c.String(nullable: false, maxLength: 128),
                    SurveyId = c.String(maxLength: 128),
                    UserId = c.String(maxLength: 128),
                    StudyGroupId = c.String(maxLength: 128),
                    UserResponseText = c.String(),
                    SurveyResponseReceivedTime = c.String(),
                })
                .PrimaryKey(t => t.SurveyResponseId)
                .ForeignKey("dbo.StudyGroups", t => t.StudyGroupId)
                .ForeignKey("dbo.Surveys", t => t.SurveyId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.SurveyId)
                .Index(t => t.UserId)
                .Index(t => t.StudyGroupId);

            CreateTable(
                "dbo.Surveys",
                c => new
                {
                    SurveyId = c.String(nullable: false, maxLength: 128),
                    QuestionText = c.String(),
                    StudyGroupId = c.String(maxLength: 128),
                    SurveyCreatedTime = c.String(),
                })
                .PrimaryKey(t => t.SurveyId)
                .ForeignKey("dbo.StudyGroups", t => t.StudyGroupId)
                .Index(t => t.StudyGroupId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.SurveyResponses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SurveyResponses", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.Surveys", "StudyGroupId", "dbo.StudyGroups");
            DropForeignKey("dbo.SurveyResponses", "StudyGroupId", "dbo.StudyGroups");
            DropForeignKey("dbo.StudyGroups", "StudyGroupId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Surveys", new[] { "StudyGroupId" });
            DropIndex("dbo.SurveyResponses", new[] { "StudyGroupId" });
            DropIndex("dbo.SurveyResponses", new[] { "UserId" });
            DropIndex("dbo.SurveyResponses", new[] { "SurveyId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.StudyGroups", new[] { "StudyGroupId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Surveys");
            DropTable("dbo.SurveyResponses");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.StudyGroups");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
