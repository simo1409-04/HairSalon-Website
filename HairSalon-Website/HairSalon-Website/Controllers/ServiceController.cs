using HairSalon.Data;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Data.Models;

namespace HairSalon_Website.Controllers
{
    public class ServiceController : Controller
    {
        private HairSalonDb context;

        public ServiceController(HairSalonDb context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {

            List<Service> services = context.Services.ToList();


            return View(services);
        }
    }
}
