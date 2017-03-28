using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Models.Entities;
using Models.Migrations;

namespace Models
{
    public class AppContext : DbContext, IDisposable
    {
        public AppContext()
            : base($"name=App")
        {
            System.Diagnostics.Debug.WriteLine("Created AppContext");
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppContext, Configuration>());
        }

        public AppContext(string connectionStringName)
            : base(connectionStringName)
        {
        }


        public AppContext(string connectionStringName, bool dropDatabase)
            : base($"name={connectionStringName}")
        {
            if (dropDatabase)
            {
                Database.SetInitializer(new DropCreateDatabaseAlways<AppContext>());
            }
            else
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppContext, Configuration>());
            }

        }

        public new void Dispose()
        {
            System.Diagnostics.Debug.WriteLine("Disposing AppContext");
            base.Dispose();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityUserVoice> ActivityUserVoices { get; set; }
        public virtual DbSet<Interest> Interests { get; set; }
        public virtual DbSet<Organizer> Organizers { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
