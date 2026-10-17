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
                new { ReservationsId = 3, ServicesId = 1 },
                new { ReservationsId = 4, ServicesId = 4 },
                new { ReservationsId = 5, ServicesId = 5 },
                new { ReservationsId = 6, ServicesId = 2 },
                new { ReservationsId = 6, ServicesId = 6 },
                new { ReservationsId = 7, ServicesId = 3 },
                new { ReservationsId = 7, ServicesId = 7 },
                new { ReservationsId = 8, ServicesId = 1 },
                new { ReservationsId = 8, ServicesId = 8 },
                new { ReservationsId = 9, ServicesId = 2 },
                new { ReservationsId = 10, ServicesId = 1 },
                new { ReservationsId = 11, ServicesId = 3 },
                new { ReservationsId = 11, ServicesId = 6 },
                new { ReservationsId = 12, ServicesId = 5 },
                new { ReservationsId = 12, ServicesId = 8 },

                new { ReservationsId = 13, ServicesId = 1 },
                new { ReservationsId = 14, ServicesId = 3 },
                new { ReservationsId = 15, ServicesId = 2 },
                new { ReservationsId = 15, ServicesId = 7 },
                new { ReservationsId = 16, ServicesId = 4 },
                new { ReservationsId = 16, ServicesId = 6 },

                new { ReservationsId = 17, ServicesId = 5 },
                new { ReservationsId = 18, ServicesId = 1 },
                new { ReservationsId = 18, ServicesId = 6 },
                new { ReservationsId = 19, ServicesId = 2 },
                new { ReservationsId = 20, ServicesId = 3 },
                new { ReservationsId = 20, ServicesId = 8 },

                new { ReservationsId = 21, ServicesId = 1 },
                new { ReservationsId = 22, ServicesId = 4 },
                new { ReservationsId = 23, ServicesId = 2 },
                new { ReservationsId = 23, ServicesId = 6 },
                new { ReservationsId = 24, ServicesId = 5 },

                new { ReservationsId = 25, ServicesId = 3 },
                new { ReservationsId = 26, ServicesId = 1 },
                new { ReservationsId = 26, ServicesId = 8 },
                new { ReservationsId = 27, ServicesId = 2 },
                new { ReservationsId = 28, ServicesId = 4 },

                new { ReservationsId = 29, ServicesId = 5 },
                new { ReservationsId = 29, ServicesId = 6 },
                new { ReservationsId = 30, ServicesId = 1 },
                new { ReservationsId = 31, ServicesId = 3 },
                new { ReservationsId = 31, ServicesId = 7 },
                new { ReservationsId = 32, ServicesId = 2 },

                new { ReservationsId = 33, ServicesId = 1 },
                new { ReservationsId = 33, ServicesId = 6 },
                new { ReservationsId = 34, ServicesId = 4 },
                new { ReservationsId = 35, ServicesId = 5 },
                new { ReservationsId = 36, ServicesId = 3 },
                new { ReservationsId = 36, ServicesId = 8 },
                new { ReservationsId = 37, ServicesId = 2 },
                new { ReservationsId = 37, ServicesId = 6 },
                new { ReservationsId = 37, ServicesId = 7 },
                new { ReservationsId = 38, ServicesId = 1 }
            );
        }

    }
}
