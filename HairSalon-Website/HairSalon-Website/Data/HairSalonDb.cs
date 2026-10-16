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



            modelBuilder.Entity("ReservationService").HasData(
    new { ReservationsId = 1, ServicesId = 2 },

    new { ReservationsId = 2, ServicesId = 3 },
    new { ReservationsId = 2, ServicesId = 8 },

    new { ReservationsId = 3, ServicesId = 1 },

    new { ReservationsId = 4, ServicesId = 4 },

    new { ReservationsId = 5, ServicesId = 5 },

    new { ReservationsId = 6, ServicesId = 2 },
    new { ReservationsId = 6, ServicesId = 6 },

    new { ReservationsId = 7, ServicesId = 3 },
    new { ReservationsId = 7, ServicesId = 7 },

    new { ReservationsId = 8, ServicesId = 1 },

    new { ReservationsId = 9, ServicesId = 2 },
    new { ReservationsId = 9, ServicesId = 8 },

    new { ReservationsId = 10, ServicesId = 4 },

    new { ReservationsId = 11, ServicesId = 5 },

    new { ReservationsId = 12, ServicesId = 1 },

    new { ReservationsId = 13, ServicesId = 2 },
    new { ReservationsId = 13, ServicesId = 6 },

    new { ReservationsId = 14, ServicesId = 3 },
    new { ReservationsId = 14, ServicesId = 8 },

    new { ReservationsId = 15, ServicesId = 2 },

    new { ReservationsId = 16, ServicesId = 1 },

    new { ReservationsId = 17, ServicesId = 3 },
    new { ReservationsId = 17, ServicesId = 7 },

    new { ReservationsId = 18, ServicesId = 4 },

    new { ReservationsId = 19, ServicesId = 2 },
    new { ReservationsId = 19, ServicesId = 8 },

    new { ReservationsId = 20, ServicesId = 5 }
);

        }

    }
}
