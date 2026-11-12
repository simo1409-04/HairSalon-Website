using Microsoft.AspNetCore.Mvc;
using HairSalon.Data.DTOs.Gallery;
using HairSalon.Services.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using HairSalon.Services.Core.Gallery.Enums;


namespace HairSalon_Website.Controllers
{
    public class GalleryController(IGalleryService service) : Controller
    {
        [HttpGet]
        public IActionResult ViewAll()
        {

            List<string> GalleryUrls = service.GetAllGalleryUrls();
            ViewBag.HomeGallery = service.GetAllHomeGalleryUrls();


            return View(GalleryUrls);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult ViewAllAdmin()
        {

            List<string> GalleryUrls = service.GetAllGalleryUrls();
            ViewBag.HomeGallery = service.GetAllHomeGalleryUrls();

            return View(GalleryUrls);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {

            DeleteStatus deleteStatus = service.Delete(id);

                if(deleteStatus==DeleteStatus.NotFoundImage)
            {

                TempData["ImageNotFound"] = "Изображението не беше намерено!";

                return RedirectToAction("ViewAllAdmin");
            }

else if(deleteStatus == DeleteStatus.Sucsess)
            {
                TempData["ImageDeleteSucsess"] = "Изображението беше изтрито успешно!";

                return RedirectToAction("ViewAllAdmin");
            }


            return RedirectToAction("ViewAllAdmin");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddPhoto()
        {
            return View(new GalleryDto());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPhotoConfirm(GalleryDto photoFile)
        {

            if (!ModelState.IsValid)
            {

                return RedirectToAction("AddPhoto");


            }


            AddStatus status = await service.AddPhoto(photoFile);


            if (status == AddStatus.WrongFormat)
            {
                TempData["WrongFormatGalleryAdd"] = "Грешен формат!";

                return RedirectToAction("AddPhoto");


            }
            else if (status == AddStatus.FileNotSaved)
            {
                TempData["FileNotSaved"] = "Файлът не беше запаметен!";

                return RedirectToAction("ViewAllAdmin");


            }
            else if (status == AddStatus.Sucsses)
            {
                TempData["FileSavedSucsses"] = "Файлът беше успешно запаметен!";

                return RedirectToAction("ViewAllAdmin");


            }

            return RedirectToAction("ViewAllAdmin");

        }









        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddPhotoToHomeGallery()
        {
            return View(new GalleryDto());
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPhotoToHomeGalleryConfirm(GalleryDto photoFile)
        {

            if (!ModelState.IsValid)
            {

                return RedirectToAction("AddPhotoToHomeGallery");


            }


            AddStatus status = await service.AddPhotoToHomeGallery(photoFile);


            if (status == AddStatus.WrongFormat)
            {
                TempData["WrongFormatGalleryAdd"] = "Грешен формат!";

                return RedirectToAction("AddPhotoToHomeGallery");


            }
            else if (status == AddStatus.FileNotSaved)
            {
                TempData["FileNotSaved"] = "Файлът не беше запаметен!";

                return RedirectToAction("ViewAllAdmin");


            }
            else if (status == AddStatus.Sucsses)
            {
                TempData["FileSavedSucsses"] = "Файлът беше успешно запаметен!";

                return RedirectToAction("ViewAllAdmin");


            }

            return RedirectToAction("ViewAllAdmin");

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteFromHomeGallery(string id)
        {

            DeleteStatus deleteStatus =  service.DeleteFromHomeGallery(id);

            if (deleteStatus == DeleteStatus.NotFoundImage)
            {

                TempData["ImageNotFound"] = "Изображението не беше намерено!";

                return RedirectToAction("ViewAllAdmin");
            }

            else if (deleteStatus == DeleteStatus.Sucsess)
            {
                TempData["ImageDeleteSucsess"] = "Изображението беше изтрито успешно!";

                return RedirectToAction("ViewAllAdmin");
            }


            return RedirectToAction("ViewAllAdmin");
        }
    }
}
