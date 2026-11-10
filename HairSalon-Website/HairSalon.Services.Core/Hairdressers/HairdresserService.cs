using HairSalon.Data.Contracts;
using HairSalon.Data.DTOs.HairDresser;
using HairSalon.Data.Models;
using HairSalon.Services.Core.Contracts;
using HairSalon.Services.Core.Hairdressers.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HairSalon.Services.Core.Hairdressers
{
    public class HairdresserService(
        IRepository repository,
        IWebHostEnvironment environment) : IHairdresserService
    {
        public async Task<List<HairdresserDto>> ReadAll()
        {
            return await repository.All<HairDresser>()
                .Select(x => new HairdresserDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    AcademicCertificateUrl = x.AcademicCertificateUrl,
                    ProfilePictureUrl = x.ProfilePictureUrl,
                    AdditionalInformation = x.AdditionalInformation
                })
                .ToListAsync();
        }

        public async Task<HairDresserStatus> AddHairDresser(HairDresserInputModel model)
        {
            string profileExtension =
                Path.GetExtension(model.ProfilePicture.FileName).ToLowerInvariant();

            string certificateExtension =
                Path.GetExtension(model.AcademicCertificate.FileName).ToLowerInvariant();

            bool profileFormatIsValid =
                profileExtension == ".png" ||
                profileExtension == ".jpg" ||
                profileExtension == ".jpeg";

            bool certificateFormatIsValid =
                certificateExtension == ".png" ||
                certificateExtension == ".jpg" ||
                certificateExtension == ".jpeg";

            if (!profileFormatIsValid || !certificateFormatIsValid)
            {
                return HairDresserStatus.WrongFormatError;
            }

            string profileFolder = Path.Combine(
                environment.WebRootPath,
                "images",
                "Hairdressers",
                "profile-pictures");

            string certificateFolder = Path.Combine(
                environment.WebRootPath,
                "images",
                "Hairdressers",
                "certificate-pictures");

            Directory.CreateDirectory(profileFolder);
            Directory.CreateDirectory(certificateFolder);

            string profileFileName = $"{Guid.NewGuid():N}{profileExtension}";
            string certificateFileName = $"{Guid.NewGuid():N}{certificateExtension}";

            string profileDiskPath = Path.Combine(profileFolder, profileFileName);
            string certificateDiskPath = Path.Combine(certificateFolder, certificateFileName);

            await using (var stream = File.Create(profileDiskPath))
            {
                await model.ProfilePicture.CopyToAsync(stream);
            }

            await using (var stream = File.Create(certificateDiskPath))
            {
                await model.AcademicCertificate.CopyToAsync(stream);
            }

            var hairdresser = new HairDresser
            {
                Name = model.Name,
                AdditionalInformation = model.AdditionalInformation,

                ProfilePictureUrl =
                    $"/images/Hairdressers/profile-pictures/{profileFileName}",
                AcademicCertificateUrl =
                    $"/images/Hairdressers/certificate-pictures/{certificateFileName}"
            };

            try
            {
                repository.Add<HairDresser>(hairdresser);
                await repository.SaveChangesAsync();

                return HairDresserStatus.Success;
            }
            catch
            {
                return HairDresserStatus.DatabaseError;
            }
        }
    }
}