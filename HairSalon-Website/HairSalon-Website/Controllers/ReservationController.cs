using HairSalon.Data;
using HairSalon.Data.Models;
using Microsoft.AspNetCore.Mvc;
using HairSalon.DataTransferObjects.DTOs_Import;

namespace HairSalon_Website.Controllers
{
    public class ReservationController : Controller
    {
        private HairSalonDb context;
        private List<TimeOnly> validSlots = new List<TimeOnly>
{
    new TimeOnly(10, 0),
    new TimeOnly(10, 50),
    new TimeOnly(11, 40),
    new TimeOnly(12, 30),

    new TimeOnly(14, 0),
    new TimeOnly(14, 50),
    new TimeOnly(15, 40),
    new TimeOnly(16, 30),
    new TimeOnly(17, 20),
    new TimeOnly(18, 10),
    new TimeOnly(19, 0),
};

        private List<DayOfWeek> nonWorkingDays = new List<DayOfWeek>
{


            DayOfWeek.Monday,
            DayOfWeek.Tuesday


};

        public ReservationController(HairSalonDb context)
        {
            this.context = context;

        }
        [HttpGet]
        public IActionResult Create()
        {
            LoadCreateViewData();

            return View();
        }

        [HttpPost]
        public IActionResult Create(ReservationDtoImport reservationDto, List<int> serviceIds)
        {


            ViewBag.ErrorMessage = null;


            List<int> serviceIdsInDb = context.Services.Select(x => x.Id).ToList();
            List<int> HairDresserIdsInDb = context.HairDressers.Select(x => x.Id).ToList();

            var today = DateOnly.FromDateTime(DateTime.Today);
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);

            bool areServiceIdsValid =
                serviceIds.Any() &&
                serviceIds.All(id => serviceIdsInDb.Contains(id));
            bool isModelStateValid = ModelState.IsValid;
            bool isHairDresserIdIsValid = HairDresserIdsInDb.Contains(reservationDto.HairDresserId);
            bool isSlotAvailable = !context.Reservations.Any(x => x.ReservationDate == reservationDto.ReservationDate && x.ReservationTime == reservationDto.ReservationTime && x.HairDresserId == reservationDto.HairDresserId);
            bool isDayOfWeekForReservationValid = !nonWorkingDays.Contains(reservationDto.ReservationDate.DayOfWeek);

            if (areServiceIdsValid && isModelStateValid && isHairDresserIdIsValid&& isSlotAvailable&& isDayOfWeekForReservationValid)
            {

                if (reservationDto.ReservationDate > today ||
              (reservationDto.ReservationDate == today &&
               reservationDto.ReservationTime >currentTime))
                {

                    bool isSlotValid = validSlots.Contains(reservationDto.ReservationTime);

                    decimal CorrectTotalPricePrice = context.Services.Where(x => serviceIds.Contains(x.Id)).Select(x => x.Price).Sum();


                    if(isSlotValid)
                    {

                        List<Service> servicesForReservationDb = context.Services.Where(x => serviceIds.Contains(x.Id)).ToList();
                        HairDresser? hairDresser = context.HairDressers.FirstOrDefault(x => x.Id == reservationDto.HairDresserId);

                        Reservation reservationForDb = new Reservation
                        {
                            ClientName = reservationDto.ClientName,
                            ClientPhone = reservationDto.ClientPhone,
                            ReservationDate = reservationDto.ReservationDate,
                            ReservationTime = reservationDto.ReservationTime,
                            HairDresser = hairDresser,
                            TotalPrice = CorrectTotalPricePrice,
                            Services = servicesForReservationDb
                        };

                        context.Reservations.Add(reservationForDb);

                        try
                        {
                            context.SaveChanges();

                            

                            

                            return View("Result", reservationForDb);


                        }

                        catch (Exception)
                        {
                            TempData["ErrorMessage"] = "Резервацията не беше запаметена. Моля, опитай отново.";


                            return RedirectToAction("Index", "Home");
                        }
                       





                    }

                }

            }
            LoadCreateViewData();


            return View(reservationDto);


        }

        [HttpGet]
        public IActionResult GetHairdresserBusySlots(int id)
        {

            var hairdresserSlots = context.Reservations.Where(x => x.HairDresserId == id).Select(x => new { x.ReservationDate, x.ReservationTime }).ToList();


            Dictionary<DateOnly,TimeOnly[]> hairdresserSlotsDic = hairdresserSlots.GroupBy(x => x.ReservationDate).ToDictionary(group => group.Key, group => group.Select(x=>x.ReservationTime).ToArray());

            return Json(hairdresserSlotsDic);

        }


        private void LoadCreateViewData()
        {
            ViewBag.Services = context.Services
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                })
                .ToList();

            ViewBag.HairDressers = context.HairDressers
                .Select(x => new
                {
                    Name = x.Name,
                    Value = x.Id
                })
                .ToList();
        }

        

    }
}
