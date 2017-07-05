namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class id_for_user : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
        }
    }
}
