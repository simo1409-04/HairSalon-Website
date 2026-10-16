using HairSalon_Website.Data.Enums;
using HairSalon_Website.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairSalon_Website.Data.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {

        public List<Reservation> Reservations = new List<Reservation>
{
    new Reservation
    {
        Id = 1,
        ClientName = "Иван",
        ClientPhone = "0888123456",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(10, 0),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 2,
        ClientName = "Петър",
        ClientPhone = "0888123457",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(11, 40),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 3,
        ClientName = "Георги",
        ClientPhone = "0888123458",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(14, 0),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 4,
        ClientName = "Николай",
        ClientPhone = "0888123459",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(17, 20),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 5,
        ClientName = "Димитър",
        ClientPhone = "0888123460",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(19, 0),
        HairDresserId = 1
    },

    new Reservation
    {
        Id = 6,
        ClientName = "Алекс",
        ClientPhone = "0888123461",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(10, 50),
        HairDresserId = 2
    },
    new Reservation
    {
        Id = 7,
        ClientName = "Борис",
        ClientPhone = "0888123462",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(12, 30),
        HairDresserId = 2
    },
    new Reservation
    {
        Id = 8,
        ClientName = "Виктор",
        ClientPhone = "0888123463",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(15, 40),
        HairDresserId = 2
    },

    new Reservation
    {
        Id = 9,
        ClientName = "Мартин",
        ClientPhone = "0888123464",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(10, 0),
        HairDresserId = 3
    },
    new Reservation
    {
        Id = 10,
        ClientName = "Стефан",
        ClientPhone = "0888123465",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(14, 50),
        HairDresserId = 3
    },
    new Reservation
    {
        Id = 11,
        ClientName = "Калин",
        ClientPhone = "0888123466",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(18, 10),
        HairDresserId = 3
    },

    new Reservation
    {
        Id = 12,
        ClientName = "Антон",
        ClientPhone = "0888123467",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(10, 50),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 13,
        ClientName = "Тодор",
        ClientPhone = "0888123468",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(16, 30),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 14,
        ClientName = "Румен",
        ClientPhone = "0888123469",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(19, 50),
        HairDresserId = 1
    },

    new Reservation
    {
        Id = 15,
        ClientName = "Стоян",
        ClientPhone = "0888123470",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(10, 0),
        HairDresserId = 2
    },
    new Reservation
    {
        Id = 16,
        ClientName = "Любомир",
        ClientPhone = "0888123471",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(14, 0),
        HairDresserId = 2
    },
    new Reservation
    {
        Id = 17,
        ClientName = "Васил",
        ClientPhone = "0888123472",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(17, 20),
        HairDresserId = 2
    },

    new Reservation
    {
        Id = 18,
        ClientName = "Красимир",
        ClientPhone = "0888123473",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(11, 40),
        HairDresserId = 3
    },
    new Reservation
    {
        Id = 19,
        ClientName = "Радослав",
        ClientPhone = "0888123474",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(15, 40),
        HairDresserId = 3
    },
    new Reservation
    {
        Id = 20,
        ClientName = "Милен",
        ClientPhone = "0888123475",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(19, 0),
        HairDresserId = 3
    }
};
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasData(Reservations);
            builder.HasIndex(x => new
            {
                x.HairDresserId,
                x.ReservationDate,
                x.ReservationTime
            })
.IsUnique();

        }

    }
}
