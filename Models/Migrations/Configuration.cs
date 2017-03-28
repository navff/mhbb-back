namespace Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; // ������������ ��������, ���� ���� ��� ������� � ������ ������
        }

        protected override void Seed(Models.AppContext context)
        {
            Seeder.Seed(context);
        }
    }
}
