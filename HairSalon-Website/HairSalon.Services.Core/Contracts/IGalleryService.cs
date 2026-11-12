
using HairSalon.Data.DTOs.Gallery;
using HairSalon.Services.Core.Gallery.Enums;
using System;
using System.Collections.Generic;
using System.Text;


namespace HairSalon.Services.Core.Contracts
{
    public interface IGalleryService
    {

        public List<string> GetAllGalleryUrls();
        public DeleteStatus Delete(string id);

        public Task<AddStatus> AddPhoto(GalleryDto filePhoto);

        public List<string> GetAllHomeGalleryUrls();

        public DeleteStatus DeleteFromHomeGallery(string id);
        public Task<AddStatus> AddPhotoToHomeGallery(GalleryDto filePhoto);


    }
}
