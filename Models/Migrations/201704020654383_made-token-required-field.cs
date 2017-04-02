namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madetokenrequiredfield : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "AuthToken", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "AuthToken", c => c.String());
        }
    }
}
