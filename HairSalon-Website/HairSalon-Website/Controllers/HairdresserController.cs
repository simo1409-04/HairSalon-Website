using HairSalon.Data.DTOs.HairDresser;
using HairSalon.Services.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Services.Core.Hairdressers.Enums;

namespace HairSalon_Website.Controllers
{
    public class HairdresserController(IHairdresserService hairdresserService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> All()
        {

            List<HairdresserDto> hairdressers = await hairdresserService.ReadAll();

            return View(hairdressers);
        }


        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AllAdmin()
        {

            List<HairdresserDto> hairdressers = await hairdresserService.ReadAll();

            return View(hairdressers);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Add()
        {


            return View();
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddConfirm(HairDresserInputModel model)
        {

            if(!ModelState.IsValid)
            {
                return View("Add", model);


            }


            HairDresserStatus status=   await hairdresserService.AddHairDresser(model);


             if (status == HairDresserStatus.WrongFormatError)
            {
                TempData["ErrorWrongFormat"] = "Качен е грешен формат на едно от изображенията!";

                return View("Add", model);



            }

            else if (status == HairDresserStatus.DatabaseError)
            {
                TempData["ErrorSavingHairDresser"] = "Фризъорът не можа да бъде запаметен!";
                return RedirectToAction("AllAdmin");
            }

            else if (status == HairDresserStatus.Success)
            {
                TempData["SucsessSavingHairDresser"] = "Фризъорът бъше запаметен!";
                return RedirectToAction("AllAdmin");
            }

            return View("Add", model);
        }
    }
}
