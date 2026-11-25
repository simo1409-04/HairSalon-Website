using HairSalon.Data.Contracts;
using HairSalon.Data.DTOs.HairDresser;
using HairSalon.Data.Models;
using HairSalon.Services.Core.Contracts;
using HairSalon.Services.Core.Hairdressers.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SalonMapper = HairSalon.Services.Core.Mapper.Mapper;

namespace HairSalon.Services.Core.Hairdressers
{
    public class HairdresserService(
        IRepository repository,
        IWebHostEnvironment environment,
        SalonMapper mapper) : IHairdresserService
    {
        public async Task<List<HairdresserDto>> ReadAll()
        {
            return await mapper.ProjectHairdresserDtos(repository.All<HairDresser>())
                .ToListAsync();
        }

        public async Task<HairDresserStatus> AddHairDresser(HairDresserInputModel model)
        {

            bool isEmailUnique = !await repository.All<HairDresser>()
                .AnyAsync(x => x.Email == model.Email);


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



            if(!isEmailUnique)
            {
                return HairDresserStatus.EmailAlreadyExists;
            }

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

            try
            {
                await using (var stream = File.Create(profileDiskPath))
                {
                    await model.ProfilePicture.CopyToAsync(stream);
                }
            }
            catch(Exception)
            {

                if(File.Exists(profileDiskPath))
                {
                    File.Delete(profileDiskPath);
                }
                return HairDresserStatus.FileNotCompletlyCreated;
            }
            try
            {

                await using (var stream = File.Create(certificateDiskPath))
                {
                    await model.AcademicCertificate.CopyToAsync(stream);
                }
            }
            catch(Exception)
            {

                if (File.Exists(certificateDiskPath))
                {
                    File.Delete(certificateDiskPath);
                }
                if (File.Exists(profileDiskPath))
                {
                    File.Delete(profileDiskPath);
                }
                return HairDresserStatus.FileNotCompletlyCreated;

            }
           

            var hairdresser = new HairDresser
            {
                Name = model.Name,
                AdditionalInformation = model.AdditionalInformation ?? string.Empty,

                ProfilePictureUrl =
                    $"/images/Hairdressers/profile-pictures/{profileFileName}",
                AcademicCertificateUrl =
                    $"/images/Hairdressers/certificate-pictures/{certificateFileName}", 
                Email = model.Email
            };

            try
            {
                repository.Add<HairDresser>(hairdresser);
                await repository.SaveChangesAsync();

                return HairDresserStatus.Success;
            }
            catch
            {
                if (File.Exists(certificateDiskPath))
                {
                    File.Delete(certificateDiskPath);
                }
                if (File.Exists(profileDiskPath))
                {
                    File.Delete(profileDiskPath);
                }
                return HairDresserStatus.DatabaseError;
            }
        }


        public async Task<HairdresserEdit> FindHairdresserById(int id)
        {



            HairdresserEdit hairDresser = await mapper
                .ProjectHairdresserEdits(repository.All<HairDresser>().Where(x => x.Id == id))
                .FirstOrDefaultAsync();

            return hairDresser;

        }

        public async Task<EditStatus> EditHairdresserById(int id, HairdresserEdit model)
        {
            HairDresser? hairDresserForDb = await repository.All<HairDresser>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (hairDresserForDb == null)
            {
                return EditStatus.IdNotFound;
            }

            bool isEmailUnique = !await repository.All<HairDresser>()
                .AnyAsync(x => x.Email == model.Email && x.Id != id);



            string profilePictureRootFolderPath = Path.Combine(
                environment.WebRootPath, "images", "Hairdressers", "profile-pictures");

            string certificatePictureRootFolderPath = Path.Combine(
                environment.WebRootPath, "images", "Hairdressers", "certificate-pictures");

            string oldFileUrlProfilePicture = hairDresserForDb.ProfilePictureUrl;
            string oldFileUrlCertificatePicture = hairDresserForDb.AcademicCertificateUrl;

            bool isProfilePictureForEdit = model.ProfilePicture != null;
            bool isCertificatePictureForEdit = model.AcademicCertificate != null;

            string fileExtension = "";
            string newFileNameProfilePicture = "";
            string newFileNameCertificatePicture = "";

            string fullFilePathForProfilePicture = "";

            if(!isEmailUnique)
            {
                return EditStatus.EmailAlreadyExists;
            }

            if (isProfilePictureForEdit)
            {
                fileExtension = Path.GetExtension(model.ProfilePicture!.FileName).ToLowerInvariant();

                bool profileFormatIsValid =
                    fileExtension == ".png" ||
                    fileExtension == ".jpg" ||
                    fileExtension == ".jpeg";

                if (!profileFormatIsValid)
                {
                    return EditStatus.WrongFormatError;
                }
            }

            if (isCertificatePictureForEdit)
            {
                fileExtension = Path.GetExtension(model.AcademicCertificate!.FileName).ToLowerInvariant();

                bool certificateFormatIsValid =
                    fileExtension == ".png" ||
                    fileExtension == ".jpg" ||
                    fileExtension == ".jpeg";

                if (!certificateFormatIsValid)
                {
                    return EditStatus.WrongFormatError;
                }
            }

            if (isProfilePictureForEdit)
            {
                fileExtension = Path.GetExtension(model.ProfilePicture!.FileName).ToLowerInvariant();
                newFileNameProfilePicture = $"{Guid.NewGuid():N}{fileExtension}";

                fullFilePathForProfilePicture = Path.Combine(
                    profilePictureRootFolderPath, newFileNameProfilePicture);

                try
                {
                    Directory.CreateDirectory(profilePictureRootFolderPath);
                    using (var file = File.Create(fullFilePathForProfilePicture))
                    {
                        await model.ProfilePicture.CopyToAsync(file);
                    }
                }
                catch
                {
                    if (File.Exists(fullFilePathForProfilePicture))
                    {
                        File.Delete(fullFilePathForProfilePicture);
                    }

                    return EditStatus.FileNotCompletlyCreated;
                }
            }

            if (isCertificatePictureForEdit)
            {
                fileExtension = Path.GetExtension(model.AcademicCertificate!.FileName).ToLowerInvariant();
                newFileNameCertificatePicture = $"{Guid.NewGuid():N}{fileExtension}";

                string fullFilePathForCertificatePicture = Path.Combine(
                    certificatePictureRootFolderPath, newFileNameCertificatePicture);

                try
                {
                    Directory.CreateDirectory(certificatePictureRootFolderPath);
                    using (var file = File.Create(fullFilePathForCertificatePicture))
                    {
                        await model.AcademicCertificate.CopyToAsync(file);
                    }
                }
                catch
                {
                    if (File.Exists(fullFilePathForCertificatePicture))
                    {
                        File.Delete(fullFilePathForCertificatePicture);
                    }

                    if (isProfilePictureForEdit &&
                        File.Exists(fullFilePathForProfilePicture))
                    {
                        File.Delete(fullFilePathForProfilePicture);
                    }

                    return EditStatus.FileNotCompletlyCreated;
                }
            }

            string profilePictureUrlForDb = isProfilePictureForEdit
                ? $"/images/Hairdressers/profile-pictures/{newFileNameProfilePicture}"
                : hairDresserForDb.ProfilePictureUrl;

            string certificatePictureUrlForDb = isCertificatePictureForEdit
                ? $"/images/Hairdressers/certificate-pictures/{newFileNameCertificatePicture}"
                : hairDresserForDb.AcademicCertificateUrl;

            hairDresserForDb.Name = model.Name;
            hairDresserForDb.AdditionalInformation = model.AdditionalInformation ?? string.Empty;
            hairDresserForDb.ProfilePictureUrl = profilePictureUrlForDb;
            hairDresserForDb.AcademicCertificateUrl = certificatePictureUrlForDb;
            hairDresserForDb.Email = model.Email;

            try
            {
                await repository.SaveChangesAsync();
            }
            catch
            {
                if (isProfilePictureForEdit &&
                    File.Exists(fullFilePathForProfilePicture))
                {
                    File.Delete(fullFilePathForProfilePicture);
                }

                if (isCertificatePictureForEdit)
                {
                    string fullPathOfNewCertificatePicture = Path.Combine(
                        certificatePictureRootFolderPath, newFileNameCertificatePicture);

                    if (File.Exists(fullPathOfNewCertificatePicture))
                    {
                        File.Delete(fullPathOfNewCertificatePicture);
                    }
                }

                return EditStatus.DatabaseError;
            }

            if (isProfilePictureForEdit)
            {
                string fullPathOfOldProfilePicture = Path.Combine(
                    profilePictureRootFolderPath,
                    Path.GetFileName(oldFileUrlProfilePicture));

                if (File.Exists(fullPathOfOldProfilePicture))
                {
                    File.Delete(fullPathOfOldProfilePicture);
                }
            }

            if (isCertificatePictureForEdit)
            {
                string fullPathOfOldCertificatePicture = Path.Combine(
                    certificatePictureRootFolderPath,
                    Path.GetFileName(oldFileUrlCertificatePicture));

                if (File.Exists(fullPathOfOldCertificatePicture))
                {
                    File.Delete(fullPathOfOldCertificatePicture);
                }
            }

            return EditStatus.Success;
        }


        public async Task<DeleteStatus> Delete(int id)
        {

            var hairdresser = await repository.All<HairDresser>().FirstOrDefaultAsync(x => x.Id==id);


            if(hairdresser==null)
            {

                return DeleteStatus.IdNotFound;

            }


            try
            {
                repository.Remove<HairDresser>(hairdresser);

               await  repository.SaveChangesAsync();

                return DeleteStatus.Sucsess;
            }

            catch(Exception ex)
            {


                return DeleteStatus.DataBaseError;
            }
        }
    }
}
