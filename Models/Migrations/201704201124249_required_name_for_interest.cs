namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required_name_for_interest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Interests", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Interests", "Name", c => c.String());
        }
    }
}
