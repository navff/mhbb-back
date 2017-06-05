namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_session_id_from_tempFile : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TempFiles", "SessionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TempFiles", "SessionId", c => c.String());
        }
    }
}
