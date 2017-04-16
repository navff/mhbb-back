namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_required_to_organizer : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Organizers", new[] { "Name" });
            AlterColumn("dbo.Organizers", "Name", c => c.String(nullable: false, maxLength: 150));
            CreateIndex("dbo.Organizers", "Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Organizers", new[] { "Name" });
            AlterColumn("dbo.Organizers", "Name", c => c.String(maxLength: 150));
            CreateIndex("dbo.Organizers", "Name");
        }
    }
}
