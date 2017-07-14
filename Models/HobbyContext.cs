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
    public class HobbyContext : DbContext, IDisposable
    {
        public HobbyContext()
            : base($"name=App")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HobbyContext, Configuration>());
            this.Configuration.ProxyCreationEnabled = false;
        }

        public HobbyContext(string connectionStringName)
            : base(connectionStringName)
        {
        }


        public HobbyContext(string connectionStringName, bool dropDatabase)
            : base($"name={connectionStringName}")
        {
            if (dropDatabase)
            {
                Database.SetInitializer(new DropCreateDatabaseAlways<HobbyContext>());
            }
            else
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<HobbyContext, Configuration>());
            }
        }

        public new void Dispose()
        {
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
        public virtual DbSet<TempFile> TempFiles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Review>()
                .HasOptional(r => r.ReplyTo)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.City)
                .WithMany()
                .HasForeignKey(u => u.CityId)
                .WillCascadeOnDelete(false);
        }
    }
}
