namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_index_to_organizer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organizers", "Name", c => c.String(maxLength: 150));
            CreateIndex("dbo.Organizers", "Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Organizers", new[] { "Name" });
            AlterColumn("dbo.Organizers", "Name", c => c.String());
        }
    }
}
