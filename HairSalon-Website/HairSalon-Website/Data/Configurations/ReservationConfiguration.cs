using HairSalon_Website.Data.Enums;
using HairSalon_Website.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairSalon_Website.Data.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {

        public List<Reservation> Reservations = new()
{
    new()
    {
        Id = 1,
        ClientName = "Иван",
        ClientPhone = "0888123456",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(10, 0),
        HairDresserId = 1,
        TotalPrice = 15.00m
    },
    new()
    {
        Id = 2,
        ClientName = "Петър",
        ClientPhone = "0888123457",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(10, 50),
        HairDresserId = 1,
        TotalPrice = 8.00m
    },
    new()
    {
        Id = 3,
        ClientName = "Георги",
        ClientPhone = "0888123458",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(11, 40),
        HairDresserId = 1,
        TotalPrice = 20.00m
    },
    new()
    {
        Id = 4,
        ClientName = "Николай",
        ClientPhone = "0888123459",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(12, 30),
        HairDresserId = 1,
        TotalPrice = 10.00m
    },
    new()
    {
        Id = 5,
        ClientName = "Димитър",
        ClientPhone = "0888123460",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(14, 0),
        HairDresserId = 1,
        TotalPrice = 10.00m
    },
    new()
    {
        Id = 6,
        ClientName = "Алекс",
        ClientPhone = "0888123461",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(14, 50),
        HairDresserId = 1,
        TotalPrice = 18.00m
    },
    new()
    {
        Id = 7,
        ClientName = "Борис",
        ClientPhone = "0888123462",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(15, 40),
        HairDresserId = 1,
        TotalPrice = 11.00m
    },
    new()
    {
        Id = 8,
        ClientName = "Виктор",
        ClientPhone = "0888123463",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(16, 30),
        HairDresserId = 1,
        TotalPrice = 23.00m
    },
    new()
    {
        Id = 9,
        ClientName = "Мартин",
        ClientPhone = "0888123464",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(17, 20),
        HairDresserId = 1,
        TotalPrice = 15.00m
    },
    new()
    {
        Id = 10,
        ClientName = "Стефан",
        ClientPhone = "0888123465",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(18, 10),
        HairDresserId = 1,
        TotalPrice = 20.00m
    },
    new()
    {
        Id = 11,
        ClientName = "Калин",
        ClientPhone = "0888123466",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(19, 0),
        HairDresserId = 1,
        TotalPrice = 11.00m
    },
    new()
    {
        Id = 12,
        ClientName = "Антон",
        ClientPhone = "0888123467",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(19, 50),
        HairDresserId = 1,
        TotalPrice = 13.00m
    },

    new()
    {
        Id = 13,
        ClientName = "Стоян",
        ClientPhone = "0888123468",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(10, 50),
        HairDresserId = 2,
        TotalPrice = 20.00m
    },
    new()
    {
        Id = 14,
        ClientName = "Любомир",
        ClientPhone = "0888123469",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(12, 30),
        HairDresserId = 2,
        TotalPrice = 8.00m
    },
    new()
    {
        Id = 15,
        ClientName = "Васил",
        ClientPhone = "0888123470",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(16, 30),
        HairDresserId = 2,
        TotalPrice = 18.00m
    },
    new()
    {
        Id = 16,
        ClientName = "Красимир",
        ClientPhone = "0888123471",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(19, 0),
        HairDresserId = 2,
        TotalPrice = 13.00m
    },

    new()
    {
        Id = 17,
        ClientName = "Радослав",
        ClientPhone = "0888123472",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(10, 0),
        HairDresserId = 3,
        TotalPrice = 10.00m
    },
    new()
    {
        Id = 18,
        ClientName = "Милен",
        ClientPhone = "0888123473",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(14, 0),
        HairDresserId = 3,
        TotalPrice = 23.00m
    },
    new()
    {
        Id = 19,
        ClientName = "Тодор",
        ClientPhone = "0888123474",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(17, 20),
        HairDresserId = 3,
        TotalPrice = 15.00m
    },
    new()
    {
        Id = 20,
        ClientName = "Румен",
        ClientPhone = "0888123475",
        ReservationDate = new DateOnly(2026, 10, 20),
        ReservationTime = new TimeOnly(19, 50),
        HairDresserId = 3,
        TotalPrice = 11.00m
    },

    new()
    {
        Id = 21,
        ClientName = "Кирил",
        ClientPhone = "0888123476",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(10, 0),
        HairDresserId = 1,
        TotalPrice = 20.00m
    },
    new()
    {
        Id = 22,
        ClientName = "Павел",
        ClientPhone = "0888123477",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(11, 40),
        HairDresserId = 1,
        TotalPrice = 10.00m
    },
    new()
    {
        Id = 23,
        ClientName = "Даниел",
        ClientPhone = "0888123478",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(14, 50),
        HairDresserId = 1,
        TotalPrice = 18.00m
    },
    new()
    {
        Id = 24,
        ClientName = "Атанас",
        ClientPhone = "0888123479",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(18, 10),
        HairDresserId = 1,
        TotalPrice = 10.00m
    },

    new()
    {
        Id = 25,
        ClientName = "Боян",
        ClientPhone = "0888123480",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(10, 0),
        HairDresserId = 2,
        TotalPrice = 8.00m
    },
    new()
    {
        Id = 26,
        ClientName = "Валентин",
        ClientPhone = "0888123481",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(12, 30),
        HairDresserId = 2,
        TotalPrice = 23.00m
    },
    new()
    {
        Id = 27,
        ClientName = "Григор",
        ClientPhone = "0888123482",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(15, 40),
        HairDresserId = 2,
        TotalPrice = 15.00m
    },
    new()
    {
        Id = 28,
        ClientName = "Емил",
        ClientPhone = "0888123483",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(19, 50),
        HairDresserId = 2,
        TotalPrice = 10.00m
    },

    new()
    {
        Id = 29,
        ClientName = "Живко",
        ClientPhone = "0888123484",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(10, 50),
        HairDresserId = 3,
        TotalPrice = 13.00m
    },
    new()
    {
        Id = 30,
        ClientName = "Захари",
        ClientPhone = "0888123485",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(14, 0),
        HairDresserId = 3,
        TotalPrice = 20.00m
    },
    new()
    {
        Id = 31,
        ClientName = "Илия",
        ClientPhone = "0888123486",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(16, 30),
        HairDresserId = 3,
        TotalPrice = 11.00m
    },
    new()
    {
        Id = 32,
        ClientName = "Йордан",
        ClientPhone = "0888123487",
        ReservationDate = new DateOnly(2026, 10, 21),
        ReservationTime = new TimeOnly(19, 0),
        HairDresserId = 3,
        TotalPrice = 15.00m
    },

    new()
    {
        Id = 33,
        ClientName = "Костадин",
        ClientPhone = "0888123488",
        ReservationDate = new DateOnly(2026, 10, 22),
        ReservationTime = new TimeOnly(11, 40),
        HairDresserId = 1,
        TotalPrice = 23.00m
    },
    new()
    {
        Id = 34,
        ClientName = "Лъчезар",
        ClientPhone = "0888123489",
        ReservationDate = new DateOnly(2026, 10, 22),
        ReservationTime = new TimeOnly(17, 20),
        HairDresserId = 1,
        TotalPrice = 10.00m
    },
    new()
    {
        Id = 35,
        ClientName = "Момчил",
        ClientPhone = "0888123490",
        ReservationDate = new DateOnly(2026, 10, 22),
        ReservationTime = new TimeOnly(14, 50),
        HairDresserId = 2,
        TotalPrice = 10.00m
    },
    new()
    {
        Id = 36,
        ClientName = "Ненчо",
        ClientPhone = "0888123491",
        ReservationDate = new DateOnly(2026, 10, 22),
        ReservationTime = new TimeOnly(18, 10),
        HairDresserId = 2,
        TotalPrice = 11.00m
    },
    new()
    {
        Id = 37,
        ClientName = "Огнян",
        ClientPhone = "0888123492",
        ReservationDate = new DateOnly(2026, 10, 22),
        ReservationTime = new TimeOnly(10, 0),
        HairDresserId = 3,
        TotalPrice = 21.00m
    },
    new()
    {
        Id = 38,
        ClientName = "Пламен",
        ClientPhone = "0888123493",
        ReservationDate = new DateOnly(2026, 10, 22),
        ReservationTime = new TimeOnly(19, 50),
        HairDresserId = 3,
        TotalPrice = 20.00m
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
