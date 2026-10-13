using HairSalon_Website.Data.Enums;
using HairSalon_Website.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairSalon_Website.Data.Configurations
{
    public class ReservationConfiguration
    {

        public List<Reservation> Reservations = new List<Reservation>
{
    new Reservation
    {
        Id = 1,
        ClientName = "Иван Петров",
        ClientPhone = "0888123456",
        ReservationDate = new DateTime(2026, 10, 20, 9, 0, 0),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 2,
        ClientName = "Мария Георгиева",
        ClientPhone = "0899123456",
        ReservationDate = new DateTime(2026, 10, 20, 10, 0, 0),
        HairDresserId = 2
    },
    new Reservation
    {
        Id = 3,
        ClientName = "Георги Иванов",
        ClientPhone = "0878123456",
        ReservationDate = new DateTime(2026, 10, 20, 9, 30, 0),
        HairDresserId = 3
    },
    new Reservation
    {
        Id = 4,
        ClientName = "Елена Димитрова",
        ClientPhone = "0887456789",
        ReservationDate = new DateTime(2026, 10, 21, 11, 0, 0),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 5,
        ClientName = "Николай Стоянов",
        ClientPhone = "0898456789",
        ReservationDate = new DateTime(2026, 10, 21, 12, 0, 0),
        HairDresserId = 2
    },
    new Reservation
    {
        Id = 6,
        ClientName = "Десислава Костова",
        ClientPhone = "0879456789",
        ReservationDate = new DateTime(2026, 10, 21, 11, 0, 0),
        HairDresserId = 3
    },
    new Reservation
    {
        Id = 7,
        ClientName = "Петър Христов",
        ClientPhone = "0887567890",
        ReservationDate = new DateTime(2026, 10, 22, 14, 0, 0),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 8,
        ClientName = "Анна Василева",
        ClientPhone = "0898567890",
        ReservationDate = new DateTime(2026, 10, 22, 9, 0, 0),
        HairDresserId = 2
    },
    new Reservation
    {
        Id = 9,
        ClientName = "Калин Тодоров",
        ClientPhone = "0879567890",
        ReservationDate = new DateTime(2026, 10, 22, 15, 0, 0),
        HairDresserId = 3
    },
    new Reservation
    {
        Id = 10,
        ClientName = "Ралица Николова",
        ClientPhone = "0887678901",
        ReservationDate = new DateTime(2026, 10, 23, 10, 0, 0),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 11,
        ClientName = "Стефан Марков",
        ClientPhone = "0898678901",
        ReservationDate = new DateTime(2026, 10, 23, 13, 0, 0),
        HairDresserId = 2
    },
    new Reservation
    {
        Id = 12,
        ClientName = "Йоана Пенева",
        ClientPhone = "0879678901",
        ReservationDate = new DateTime(2026, 10, 23, 12, 0, 0),
        HairDresserId = 3
    },
    new Reservation
    {
        Id = 13,
        ClientName = "Борислав Илиев",
        ClientPhone = "0887789012",
        ReservationDate = new DateTime(2026, 10, 24, 9, 0, 0),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 14,
        ClientName = "Кристина Ангелова",
        ClientPhone = "0898789012",
        ReservationDate = new DateTime(2026, 10, 24, 10, 30, 0),
        HairDresserId = 2
    },
    new Reservation
    {
        Id = 15,
        ClientName = "Валентин Попов",
        ClientPhone = "0879789012",
        ReservationDate = new DateTime(2026, 10, 24, 14, 0, 0),
        HairDresserId = 3
    },
    new Reservation
    {
        Id = 16,
        ClientName = "Теодора Миланова",
        ClientPhone = "0887890123",
        ReservationDate = new DateTime(2026, 10, 26, 16, 0, 0),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 17,
        ClientName = "Михаил Колев",
        ClientPhone = "0898890123",
        ReservationDate = new DateTime(2026, 10, 26, 15, 0, 0),
        HairDresserId = 2
    },
    new Reservation
    {
        Id = 18,
        ClientName = "Симона Донева",
        ClientPhone = "0879890123",
        ReservationDate = new DateTime(2026, 10, 26, 10, 0, 0),
        HairDresserId = 3
    },
    new Reservation
    {
        Id = 19,
        ClientName = "Александра Кирова",
        ClientPhone = "0888901234",
        ReservationDate = new DateTime(2026, 10, 27, 12, 0, 0),
        HairDresserId = 1
    },
    new Reservation
    {
        Id = 20,
        ClientName = "Даниел Борисов",
        ClientPhone = "0899901234",
        ReservationDate = new DateTime(2026, 10, 27, 17, 0, 0),
        HairDresserId = 2
    }
};
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasData(Reservations);
        }

    }
}
