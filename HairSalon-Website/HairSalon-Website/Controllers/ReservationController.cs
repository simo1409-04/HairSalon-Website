using HairSalon.Data;
using HairSalon.Data.Models;
using HairSalon.DataTransferObjects.Reservations;
using HairSalon.Services.Core.Contracts;
using HairSalon.Services.Core.Reservations;
using HairSalon.Services.Core.Reservations.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace HairSalon_Website.Controllers
{
    public class ReservationController(HairSalonDb context, IReservationService reservationService) : Controller
    {
       


        [HttpGet]
        public async Task<IActionResult> GetHairdresserBusySlots(int id)
        {

            Dictionary<DateOnly, TimeOnly[]> hairdresserBusySlots = await reservationService.GetHairdresserBusySlots(id);
            return Json(hairdresserBusySlots);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ViewBag.Services = await reservationService.LoadServicesForReservation();
            

            ViewBag.HairDressers = await reservationService.LoadHairdressersForReservation();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationDtoImport reservationDto, List<int> serviceIds)
        {
            ViewBag.Services = await reservationService.LoadServicesForReservation();


            ViewBag.HairDressers = await reservationService.LoadHairdressersForReservation();

            if (!ModelState.IsValid)
            {
             

                return View(reservationDto);

            }

           ReservationResult result = await reservationService.CreateReservation(reservationDto, serviceIds);

               

            if(result.Status== ReservationStatus.WrongInputDataError)
            {


                return View(reservationDto);

            }

            if(result.Status == ReservationStatus.Success)
            {

                return View("Result", result.Reservation);

            }


            else
            {
                @TempData["ErrorSaving"] = "Coud not save the reservation, please try again!";
                return RedirectToAction("Index", "Home");
            }

        }

       

       
        

    }
}
