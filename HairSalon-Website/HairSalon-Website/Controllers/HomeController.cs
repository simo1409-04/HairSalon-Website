using HairSalon.Data;
using HairSalon.Data.Models;
using HairSalon.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HairSalon_Website.Controllers
{
    public class HomeController(IServicesService servicesService, IGalleryService galleryService) : Controller
    {
       

        public async Task<IActionResult> Index()
        {

            ViewBag.Services = await servicesService.GetAllServices();
            ViewBag.HomeGallery = galleryService.GetAllHomeGalleryUrls();

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("All", "Reservation");
            }


            return View("Index");
        }
        public async Task<IActionResult> IndexAdmin()
        {
            ViewBag.Services = await servicesService.GetAllServices();
            ViewBag.HomeGallery = galleryService.GetAllHomeGalleryUrls();





            return View("Index");
        }

       



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
