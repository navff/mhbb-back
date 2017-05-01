namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class made_linked_object_required_for_picture : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pictures", "LinkedObjectId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pictures", "LinkedObjectId", c => c.Int());
        }
    }
}
