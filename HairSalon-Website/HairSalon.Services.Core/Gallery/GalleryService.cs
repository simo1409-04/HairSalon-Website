using HairSalon.Data.DTOs.Gallery;
using HairSalon.Services.Core.Contracts;
using HairSalon.Services.Core.Gallery.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HairSalon.Services.Core.Gallery
{
    public class GalleryService(IWebHostEnvironment enviroment) :IGalleryService
    {


        public List<string> GetAllGalleryUrls()
        {
            string folder = Path.Combine(enviroment.WebRootPath, "images", "gallery");

            List<string> urls = new List<string>();

            if(!Directory.Exists(folder))
            {

                return urls;
            }

            urls  = Directory.EnumerateFiles(folder)
         .Where(file => new[] { ".jpg", ".jpeg", ".png" }
             .Contains(Path.GetExtension(file).ToLowerInvariant())).OrderByDescending(file => File.GetCreationTime(file))
         .Select(file => $"/images/gallery/{Path.GetFileName(file)}")
         .ToList();



            return urls;
        }

        public DeleteStatus Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || Path.GetFileName(id) != id)
            {
                return DeleteStatus.NotFoundImage;
            }

            string FileFullPath = Path.Combine(enviroment.WebRootPath, "images", "gallery", id);

            if (File.Exists(FileFullPath))

               {
                File.Delete(FileFullPath);
                return DeleteStatus.Sucsess;
            }

            else
            {

                return DeleteStatus.NotFoundImage;
            }




        }

        public async Task<AddStatus> AddPhoto(GalleryDto filePhoto)
        {

            string folder = Path.Combine(enviroment.WebRootPath, "images", "gallery");
            string[] correctExtensions = { ".jpg", ".png", ".jpeg" };
            if (filePhoto.photo is null)
            {
                return AddStatus.WrongFormat;
            }

            string extension = Path.GetExtension(filePhoto.photo.FileName).ToLowerInvariant();

            bool isCorrectFormat = correctExtensions.Contains(extension)
                && await HasMatchingImageSignatureAsync(filePhoto.photo, extension);

            if(!isCorrectFormat)
            {


                return AddStatus.WrongFormat;
            }

            string newFileFullPath = $"{folder}/{Guid.NewGuid():N}{extension}";

            try
            {
                Directory.CreateDirectory(folder);
                await using var file = File.Create(newFileFullPath);
                await filePhoto.photo.CopyToAsync(file);

                return AddStatus.Sucsses;


            }


            catch(Exception)
            {
                if(File.Exists(newFileFullPath))
                {
                    File.Delete(newFileFullPath);


                }

                return AddStatus.FileNotSaved;

            }



        }

        public List<string> GetAllHomeGalleryUrls()
        {
            string folder = Path.Combine(enviroment.WebRootPath, "images", "HomeMiniGallery");

            List<string> urls = new List<string>();

            if (!Directory.Exists(folder))
            {

                return urls;
            }

            urls = Directory.EnumerateFiles(folder)
         .Where(file => new[] { ".jpg", ".jpeg", ".png" }
             .Contains(Path.GetExtension(file).ToLowerInvariant())).OrderByDescending(file => File.GetCreationTime(file))
         .Select(file => $"/images/HomeMiniGallery/{Path.GetFileName(file)}")
         .ToList();



            return urls;
        }


        public DeleteStatus DeleteFromHomeGallery(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || Path.GetFileName(id) != id)
            {
                return DeleteStatus.NotFoundImage;
            }

            string FileFullPath = Path.Combine(enviroment.WebRootPath, "images", "HomeMiniGallery", id);

            if (File.Exists(FileFullPath))

            {
                File.Delete(FileFullPath);
                return DeleteStatus.Sucsess;
            }

            else
            {

                return DeleteStatus.NotFoundImage;
            }




        }


        public async Task<AddStatus> AddPhotoToHomeGallery(GalleryDto filePhoto)
        {

            string folder = Path.Combine(enviroment.WebRootPath, "images", "HomeMiniGallery");
            string[] correctExtensions = { ".jpg", ".png", ".jpeg" };
            if (filePhoto.photo is null)
            {
                return AddStatus.WrongFormat;
            }

            string extension = Path.GetExtension(filePhoto.photo.FileName).ToLowerInvariant();

            bool isCorrectFormat = correctExtensions.Contains(extension)
                && await HasMatchingImageSignatureAsync(filePhoto.photo, extension);

            if (!isCorrectFormat)
            {


                return AddStatus.WrongFormat;
            }

            string newFileFullPath = $"{folder}/{Guid.NewGuid():N}{extension}";

            try
            {
                Directory.CreateDirectory(folder);
                await using var file = File.Create(newFileFullPath);
                await filePhoto.photo.CopyToAsync(file);

                return AddStatus.Sucsses;


            }


            catch (Exception)
            {
                if (File.Exists(newFileFullPath))
                {
                    File.Delete(newFileFullPath);


                }

                return AddStatus.FileNotSaved;

            }



        }

        private static async Task<bool> HasMatchingImageSignatureAsync(IFormFile photo, string extension)
        {
            if (photo.Length < 8)
            {
                return false;
            }

            byte[] header = new byte[8];
            try
            {
                await using var stream = photo.OpenReadStream();
                await stream.ReadExactlyAsync(header);
            }
            catch (Exception ex) when (ex is IOException or InvalidDataException)
            {
                return false;
            }

            if (extension == ".png")
            {
                return header.AsSpan().SequenceEqual(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 });
            }

            return header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF;
        }
    }
}
