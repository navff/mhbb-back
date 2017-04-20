namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_temp_files : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TempFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionId = c.String(),
                        FormId = c.String(),
                        Filename = c.String(nullable: false),
                        Data = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Pictures", "Filename", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Pictures", "Data", c => c.Binary(nullable: false));
            DropColumn("dbo.Pictures", "Description");
            DropColumn("dbo.Pictures", "Extension");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "Extension", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Pictures", "Description", c => c.String());
            DropColumn("dbo.Pictures", "Data");
            DropColumn("dbo.Pictures", "Filename");
            DropTable("dbo.TempFiles");
        }
    }
}
