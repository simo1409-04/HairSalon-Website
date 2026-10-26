using HairSalon.Data.Models.Enums;
using HairSalon.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairSalon.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public List<Service> Services = new List<Service>
{
    new Service
    {
        Id = 1,
        Name = "Подстригване и оформяне на брада",
        Gender = Gender.Male,
        Price = 20.00m,
        ServicePhotoExampleUrl = "/images/services/mens-cut-and-beard.png",
        Details = "Комбинирана услуга с мъжко подстригване и прецизно оформяне на брада."
    },
    new Service
    {
        Id = 2,
        Name = "Мъжко подстригване",
        Gender = Gender.Male,
        Price = 15.00m,
        ServicePhotoExampleUrl = "/images/services/mens-haircut.png",
        Details = "Мъжко подстригване, съобразено с формата на лицето и желания стил."
    },
    new Service
    {
        Id = 3,
        Name = "Оформяне на брада",
        Gender = Gender.Male,
        Price = 8.00m,
        ServicePhotoExampleUrl = "/images/services/beard-trim.png",
        Details = "Контуриране, скъсяване и оформяне на брада."
    },
    new Service
    {
        Id = 4,
        Name = "Кралско бръснене",
        Gender = Gender.Male,
        Price = 10.00m,
        ServicePhotoExampleUrl = "/images/services/royal-shave.png",
        Details = "Класическо бръснене с професионална грижа за кожата."
    },
    new Service
    {
        Id = 5,
        Name = "Детско подстригване до 12 години",
        Gender = Gender.Male,
        Price = 10.00m,
        ServicePhotoExampleUrl = "/images/services/kids-haircut.png",
        Details = "Подстригване за момчета до 12 години."
    },
    new Service
    {
        Id = 6,
        Name = "Топла кърпа",
        Gender = Gender.Male,
        Price = 3.00m,
        ServicePhotoExampleUrl = "/images/services/hot-towel.png",
        Details = "Топла кърпа за освежаване и комфорт."
    },
    new Service
    {
        Id = 7,
        Name = "Кола маска за лице, нос и уши",
        Gender = Gender.Male,
        Price = 3.00m,
        ServicePhotoExampleUrl = "/images/services/face-wax.png",
        Details = "Почистване с кола маска на лице, нос и уши."
    },
    new Service
    {
        Id = 8,
        Name = "Оформяне на вежди",
        Gender = Gender.Male,
        Price = 3.00m,
        ServicePhotoExampleUrl = "/images/services/eyebrow-shaping.png",
        Details = "Прецизно оформяне и почистване на вежди."
    }
};

        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasData(Services);
        }
    }
}
