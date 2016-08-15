namespace instinct360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ReviewTemplateName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReviewSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Single(nullable: false),
                        Name = c.String(),
                        Review_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reviews", t => t.Review_Id)
                .Index(t => t.Review_Id);
            
            CreateTable(
                "dbo.ReviewAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Single(nullable: false),
                        Question = c.String(),
                        Information = c.String(),
                        MultipleChoice = c.Boolean(nullable: false),
                        ChoiceText = c.String(),
                        ChoiceId = c.Int(nullable: false),
                        FreeText = c.String(),
                        ReviewSection_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReviewSections", t => t.ReviewSection_Id)
                .Index(t => t.ReviewSection_Id);
            
            CreateTable(
                "dbo.ReviewTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ReviewTemplateName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReviewTemplateSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Single(nullable: false),
                        Name = c.String(),
                        ReviewTemplate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReviewTemplates", t => t.ReviewTemplate_Id)
                .Index(t => t.ReviewTemplate_Id);
            
            CreateTable(
                "dbo.ReviewQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Single(nullable: false),
                        Question = c.String(),
                        Information = c.String(),
                        MultipleChoice = c.Boolean(nullable: false),
                        ReviewTemplateSection_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReviewTemplateSections", t => t.ReviewTemplateSection_Id)
                .Index(t => t.ReviewTemplateSection_Id);
            
            CreateTable(
                "dbo.ReviewQuestionChoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ReviewQuestion_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReviewQuestions", t => t.ReviewQuestion_Id)
                .Index(t => t.ReviewQuestion_Id);
            
            CreateTable(
                "dbo.ChoiceAttributeWeightings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Single(nullable: false),
                        ReviewQuestionChoice_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReviewQuestionChoices", t => t.ReviewQuestionChoice_Id)
                .Index(t => t.ReviewQuestionChoice_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ReviewTemplateSections", "ReviewTemplate_Id", "dbo.ReviewTemplates");
            DropForeignKey("dbo.ReviewQuestions", "ReviewTemplateSection_Id", "dbo.ReviewTemplateSections");
            DropForeignKey("dbo.ReviewQuestionChoices", "ReviewQuestion_Id", "dbo.ReviewQuestions");
            DropForeignKey("dbo.ChoiceAttributeWeightings", "ReviewQuestionChoice_Id", "dbo.ReviewQuestionChoices");
            DropForeignKey("dbo.ReviewSections", "Review_Id", "dbo.Reviews");
            DropForeignKey("dbo.ReviewAnswers", "ReviewSection_Id", "dbo.ReviewSections");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ChoiceAttributeWeightings", new[] { "ReviewQuestionChoice_Id" });
            DropIndex("dbo.ReviewQuestionChoices", new[] { "ReviewQuestion_Id" });
            DropIndex("dbo.ReviewQuestions", new[] { "ReviewTemplateSection_Id" });
            DropIndex("dbo.ReviewTemplateSections", new[] { "ReviewTemplate_Id" });
            DropIndex("dbo.ReviewAnswers", new[] { "ReviewSection_Id" });
            DropIndex("dbo.ReviewSections", new[] { "Review_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ChoiceAttributeWeightings");
            DropTable("dbo.ReviewQuestionChoices");
            DropTable("dbo.ReviewQuestions");
            DropTable("dbo.ReviewTemplateSections");
            DropTable("dbo.ReviewTemplates");
            DropTable("dbo.ReviewAnswers");
            DropTable("dbo.ReviewSections");
            DropTable("dbo.Reviews");
        }
    }
}
