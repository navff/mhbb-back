namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_index_to_pictures : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Pictures", "LinkedObjectId");
            CreateIndex("dbo.Pictures", "LinkedObjectType");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pictures", new[] { "LinkedObjectType" });
            DropIndex("dbo.Pictures", new[] { "LinkedObjectId" });
        }
    }
}
