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


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HairSalonDb).Assembly);

            modelBuilder.Entity("HairDresserService").HasData(
        new { HairDressersId = 1, ServicesId = 1 },
        new { HairDressersId = 1, ServicesId = 2 },
        new { HairDressersId = 1, ServicesId = 3 },

        new { HairDressersId = 2, ServicesId = 4 },
        new { HairDressersId = 2, ServicesId = 5 },
        new { HairDressersId = 2, ServicesId = 6 },

        new { HairDressersId = 3, ServicesId = 1 },
        new { HairDressersId = 3, ServicesId = 3 },
        new { HairDressersId = 3, ServicesId = 4 }
    );

            modelBuilder.Entity("ReservationService").HasData(
                new { ReservationsId = 1, ServicesId = 1 },
                new { ReservationsId = 2, ServicesId = 4 },
                new { ReservationsId = 3, ServicesId = 3 });

        }

    }
}
