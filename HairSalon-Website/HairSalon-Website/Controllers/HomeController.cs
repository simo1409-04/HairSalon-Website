using HairSalon_Website.Data;
using HairSalon_Website.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HairSalon_Website.Controllers
{
    public class HomeController : Controller
    {
        private HairSalonDb context;

        public HomeController(HairSalonDb context)
        {
            this.context = context;

        }
        public IActionResult Index()
        {

            return View();
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
