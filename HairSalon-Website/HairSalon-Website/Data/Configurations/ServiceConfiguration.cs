using HairSalon_Website.Data.Enums;
using HairSalon_Website.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairSalon_Website.Data.Configurations
{
    public class ServiceConfiguration
    {
        public List<Service> Services = new List<Service>
        {
            new Service
            {
                Id = 1,
                Name = "Мъжко подстригване",
                Gender = Gender.Male,
                DurationInMinutes = 45,
                Price = 20.00m,
                ServicePhotoExampleUrl = "/images/services/mens-haircut.png",
                Details = "Консултация, измиване, подстригване и оформяне."
            },
            new Service
            {
                Id = 2,
                Name = "Оформяне на брада",
                Gender = Gender.Male,
                DurationInMinutes = 30,
                Price = 12.00m,
                ServicePhotoExampleUrl = "/images/services/beard-trim.png",
                Details = "Оформяне, контуриране и грижа за брадата."
            },
            new Service
            {
                Id = 3,
                Name = "Мъжко подстригване и брада",
                Gender = Gender.Male,
                DurationInMinutes = 75,
                Price = 30.00m,
                ServicePhotoExampleUrl = "/images/services/mens-cut-and-beard.png",
                Details = "Комбинирана услуга за подстригване и оформяне на брада."
            },
            new Service
            {
                Id = 4,
                Name = "Дамско подстригване и сешоар",
                Gender = Gender.Female,
                DurationInMinutes = 60,
                Price = 45.00m,
                ServicePhotoExampleUrl = "/images/services/womens-haircut.png",
                Details = "Подстригване според типа коса и професионално оформяне със сешоар."
            },
            new Service
            {
                Id = 5,
                Name = "Боядисване на корени",
                Gender = Gender.Female,
                DurationInMinutes = 120,
                Price = 65.00m,
                ServicePhotoExampleUrl = "/images/services/root-coloring.png",
                Details = "Боядисване на израснали корени и завършващо оформяне."
            },
            new Service
            {
                Id = 6,
                Name = "Балеаж",
                Gender = Gender.Female,
                DurationInMinutes = 210,
                Price = 200.00m,
                ServicePhotoExampleUrl = "/images/services/balayage.png",
                Details = "Индивидуална техника за естествен светъл преливащ ефект."
            }
        };

        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasData(Services);
        }
    }
}
