namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userreviewreference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "UserEmail", "dbo.Users");
            DropIndex("dbo.Reviews", new[] { "UserEmail" });
            AlterColumn("dbo.Reviews", "UserEmail", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Reviews", "UserEmail");
            AddForeignKey("dbo.Reviews", "UserEmail", "dbo.Users", "Email", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "UserEmail", "dbo.Users");
            DropIndex("dbo.Reviews", new[] { "UserEmail" });
            AlterColumn("dbo.Reviews", "UserEmail", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reviews", "UserEmail");
            AddForeignKey("dbo.Reviews", "UserEmail", "dbo.Users", "Email");
        }
    }
}
