using HairSalon.Data;
using HairSalon.DataTransferObjects.Service;
using HairSalon.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Website.Controllers
{
    public class ServiceController(HairSalonDb context, IServicesService servicesService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<ServiceView> services = await servicesService.GetAllServicesAsync();

            return View(services);
        }
    }
}
