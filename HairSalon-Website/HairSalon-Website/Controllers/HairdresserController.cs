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

            else if (status == HairDresserStatus.FileNotCompletlyCreated)
            {
                TempData["FileNotCreated"] = "Профилната снимка или снимката за сертификата не можа да бъде записана!";
                return RedirectToAction("AllAdmin");
            }

            else if (status == HairDresserStatus.Success)
            {
                TempData["SucsessSavingHairDresser"] = "Фризъорът бъше запаметен!";
                return RedirectToAction("AllAdmin");
            }

            return View("Add", model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {

            var hairdresser = await hairdresserService.FindHairdresserById(id);

            if(hairdresser==null)
            {


                TempData["IdNotFoundEdit"] = "Фризъорът не беше  намерен!";

                return RedirectToAction("AllAdmin");
            }


            return View(hairdresser);





        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditConfirm([FromRoute]int id, [FromForm]HairdresserEdit model)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", model);


            }

            EditStatus status= await hairdresserService.EditHairdresserById(id, model);


            if(status==EditStatus.IdNotFound)
            {
                TempData["IdNotFoundEdit"] = "Фризъорът не беше  намерен!";

                return RedirectToAction("AllAdmin");

            }



            else if(status==EditStatus.WrongFormatError)
            {

                TempData["ErrorWrongFormat"] = "Фризъорът не беше  запаметен, качен е грешен формат на едно от изображенията!";

                return View("Edit", model);
            }

            else if(status==EditStatus.DatabaseError)
            {

                TempData["ErrorSavingHairDresser"] = "Фризъорът не можа да бъде запаметен!";
                return RedirectToAction("AllAdmin");

            }

            else if(status ==EditStatus.FileNotCompletlyCreated)
            {
                TempData["FileNotCreated"] = "Профилна снимка или снимката за сертификата не можа да бъде записана!";
                return RedirectToAction("AllAdmin");

            }
            else if(status==EditStatus.Success)
            {

                TempData["SucsessEditedHairDresser"] = "Фризъорът бъше редактиран успешно!";
                return RedirectToAction("AllAdmin");
            }
            return RedirectToAction("AllAdmin");

        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            DeleteStatus status = await hairdresserService.Delete(id);

            if(status==DeleteStatus.IdNotFound)
            {
                TempData["IdNotFoundEdit"] = "Фризъорът не беше  намерен!";

                return RedirectToAction("AllAdmin");

            }


            else if (status == DeleteStatus.DataBaseError)
            {
                TempData["DatabaseErrorDelete"] = "Фризъорът не можа да бъде изтрит!";

                return RedirectToAction("AllAdmin");

            }

            else if (status == DeleteStatus.Sucsess)
            {
                TempData["SucsessDelete"] = "Фризъорът беше изтрит успешно!";

                return RedirectToAction("AllAdmin");

            }


            return RedirectToAction("AllAdmin");

        }
    }
}
