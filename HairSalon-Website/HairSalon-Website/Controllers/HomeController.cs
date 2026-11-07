using HairSalon.Data;
using HairSalon.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HairSalon_Website.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("All", "Reservation");
            }


            return View("Index");
        }
        public IActionResult IndexAdmin()
        {

        

            
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
