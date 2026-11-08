using HairSalon.Data;
using HairSalon.Data.DTOs.Service;
using HairSalon.Data.Models;
using HairSalon.Services.Core.Contracts;
using HairSalon.Services.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Website.Controllers
{
    public class ServiceController(IServicesService servicesService) : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReadAll()
        {
            List<ServiceView> services = await servicesService.GetAllServices();

            return View(services);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateConfirm(ServiceView service)
        {

            if(!ModelState.IsValid)
            {
                TempData["ErrorService"] = "Услугата не беше добавена успешно!";

                return View("Create", service);

            }

         await   servicesService.CreateService(service);


            TempData["SucsessServiceAdded"] = "Услугата беше добавена успешно!";

            return RedirectToAction("ReadAll");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {

            
            ServiceView model = await servicesService.EditService(id);

            if(model==null)
            {

                TempData["ErrorServiceNotFound"] = "Услугата не беше намерена!";


                return RedirectToAction("ReadAll");
            }

            return View(model);
        }
      
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditConfirm(int id, ServiceView model)
        {

            if(!ModelState.IsValid)
            {

                return View("Edit", model);

            }


            Service reservation = await servicesService.EditAndSaveService(id, model);

            if(reservation==null)
            {
                TempData["ErrorServiceNotFound"] = "Услугата не беше намерена!";


                return RedirectToAction("ReadAll");
            }


            TempData["SucsessServiceAdded"] = "Услугата беше добавена успешно!";

            return RedirectToAction("ReadAll");


        }
      


    }
}
