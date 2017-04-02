namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userreservationsreference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "UserEmail", "dbo.Users");
            DropIndex("dbo.Reservations", new[] { "UserEmail" });
            AlterColumn("dbo.Reservations", "UserEmail", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Reservations", "UserEmail");
            AddForeignKey("dbo.Reservations", "UserEmail", "dbo.Users", "Email", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserEmail", "dbo.Users");
            DropIndex("dbo.Reservations", new[] { "UserEmail" });
            AlterColumn("dbo.Reservations", "UserEmail", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservations", "UserEmail");
            AddForeignKey("dbo.Reservations", "UserEmail", "dbo.Users", "Email");
        }
    }
}
