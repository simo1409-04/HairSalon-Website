using HairSalon_Website.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HairSalon_Website.Data
{
    public class HairSalonDb:DbContext
    {

        public DbSet<HairDresser> HairDressers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        public HairSalonDb(DbContextOptions<HairSalonDb> options):base(options)

        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}
