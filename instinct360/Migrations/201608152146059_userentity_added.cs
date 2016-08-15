namespace instinct360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userentity_added : DbMigration
    {
        public override void Up()
        {
//            Sql(@"
//truncate table ReviewAnswers; 
//truncate table ReviewSections; 
//truncate table Reviews; 
//truncate table ReviewTemplates; ");

            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Reviews", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.ReviewTemplates", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReviewTemplates", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Reviews", "UserId", c => c.Guid(nullable: false));
            DropTable("dbo.Users");
        }
    }
}
