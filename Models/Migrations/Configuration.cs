namespace Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.HobbyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; // ������������ ��������, ���� ���� ��� ������� � ������ ������
        }

        protected override void Seed(Models.HobbyContext context)
        {
            Seeder.Seed(context);
        }
    }
}
