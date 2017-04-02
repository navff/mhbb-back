namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_registered_date_to_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DateRegistered", c => c.DateTime(nullable: false));
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DateRegistered");
        }
    }
}
