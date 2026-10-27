using HairSalon.DataTransferObjects.Reservations;
using HairSalon.Services.Core.Contracts;
using HairSalon.Services.Core.Reservations.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Website.Controllers
{
    public class ReservationController(IReservationService reservationService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetHairdresserBusySlots(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var busySlots = await reservationService.GetHairdresserBusySlots(id);
            return Json(busySlots);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadFormOptionsAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Result()
        {
            if (TempData.Peek("ReservationId") is not int reservationId)
            {
                return RedirectToAction(nameof(Create));
            }

            var reservation = await reservationService.GetReservationById(reservationId);
            return reservation == null ? RedirectToAction(nameof(Create)) : View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationDtoImport reservationDto, List<int> serviceIds)
        {
            if (!ModelState.IsValid)
            {
                await LoadFormOptionsAsync(serviceIds);
                return View(reservationDto);
            }

            var result = await reservationService.CreateReservation(reservationDto, serviceIds);

            if (result.Status == ReservationStatus.Success && result.Reservation != null)
            {
                TempData["ReservationId"] = result.Reservation.Id;
                return RedirectToAction(nameof(Result));
            }

            if (result.Status == ReservationStatus.WrongInputDataError)
            {
                ModelState.AddModelError(result.ErrorField ?? string.Empty,
                    result.ErrorMessage ?? "Провери данните за резервацията.");
                await LoadFormOptionsAsync(serviceIds);
                return View(reservationDto);
            }

            TempData["ErrorMessage"] = "Резервацията не беше запазена. Опитай отново.";
            return RedirectToAction("Index", "Home");
        }

        private async Task LoadFormOptionsAsync(List<int>? selectedServiceIds = null)
        {
            ViewBag.Services = await reservationService.LoadServicesForReservation();
            ViewBag.HairDressers = await reservationService.LoadHairdressersForReservation();
            ViewBag.SelectedServiceIds = selectedServiceIds ?? new List<int>();
        }
    }
}
