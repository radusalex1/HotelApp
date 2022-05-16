using HotelApp.Models;
using System.Data.Entity;

namespace HotelApp.DBContext
{
    public class HotelContext: DbContext
    {
        public HotelContext(string conString):base(conString)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservations> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("T_Users");
            modelBuilder.Entity<Room>().ToTable("T_Rooms");
            modelBuilder.Entity<Reservations>().ToTable("T_Reservations");
        }

    }
}
