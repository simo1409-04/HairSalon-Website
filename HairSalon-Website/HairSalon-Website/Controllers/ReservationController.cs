using HairSalon_Website.Data;
using HairSalon_Website.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Website.Controllers
{
    public class ReservationController : Controller
    {
        private HairSalonDb context;

        public ReservationController(HairSalonDb context)
        {
            this.context = context;

        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.HairDressers = context.HairDressers.Select(x=>new { Name = x.Name, Value = x.Id }).ToList();
            ViewBag.Services = context.Services.Select(x => new { Name = x.Name, Value = x.Id }).ToList();



            return View();
        }
    }
}
