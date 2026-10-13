using HairSalon_Website.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairSalon_Website.Data.Configurations
{
    public class HairDresserConfiguration: IEntityTypeConfiguration<HairDresser>
    {

        public List<HairDresser> HairDressers = new List<HairDresser>
        {
new HairDresser
{
    Id = 1,
    Name = "Александър Димитров",
    YearsOfExperiance = 7,
    ProfilePictureUrl = "/images/aleksandar-dimitrov/profile-picture.png",
    AcademicCertificateUrl = "/images/aleksandar-dimitrov/certificate.png",
    AdditionalInformation = "Специализира в мъжко подстригване, оформяне на брада и класически барбер стил."
},
new HairDresser
{
    Id = 2,
    Name = "Виктория Петрова",
    YearsOfExperiance = 9,
    ProfilePictureUrl = "/images/viktoria-petrova/profile-picture.png",
    AcademicCertificateUrl = "/images/viktoria-petrova/certificate.png",
    AdditionalInformation = "Специализира в боядисване, балеаж, кичури и съвременни дамски прически."
},
new HairDresser
{
    Id = 3,
    Name = "Мартин Иванов",
    YearsOfExperiance = 4,
    ProfilePictureUrl = "/images/martin-ivanov/profile-picture.png",
    AcademicCertificateUrl = "/images/martin-ivanov/certificate.png",
    AdditionalInformation = "Работи с модерни мъжки и унисекс прически, оформяне и ежедневен стайлинг."
}
        };


        public void Configure(EntityTypeBuilder<HairDresser> builder)
        {

            builder.HasData(HairDressers);

        }
    }
}
