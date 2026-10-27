using HairSalon.Data.Models;
using HairSalon.DataTransferObjects.HairDresser;
using HairSalon.DataTransferObjects.Reservations;
using HairSalon.DataTransferObjects.Service;
using HairSalon.Services.Core.Contracts;
using HairSalon.Services.Core.Reservations.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;



namespace HairSalon.Services.Core.Reservations
{
    public  class ReservationService(IRepository repository, ReservationResult result) : IReservationService
    {
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

        private List<DayOfWeek> nonWorkingDays = new List<DayOfWeek>{


            DayOfWeek.Monday,
            DayOfWeek.Tuesday


};

        public async  Task<Dictionary<DateOnly, TimeOnly[]> >GetHairdresserBusySlots(int id)
        {

            var hairdresserSlots = await repository.All<Reservation>().Where(x => x.HairDresserId == id).Select(x => new { x.ReservationDate, x.ReservationTime }).ToListAsync();


            Dictionary<DateOnly, TimeOnly[]> hairdresserSlotsDic = hairdresserSlots.GroupBy(x => x.ReservationDate).ToDictionary(group => group.Key, group => group.Select(x => x.ReservationTime).ToArray());

            return hairdresserSlotsDic;
        }

        public async Task<List<ServiceInReservationCreateDto>> LoadServicesForReservation()
        {
        
            List<ServiceInReservationCreateDto> Services = await repository.All<Service>()
                .Select(x => new ServiceInReservationCreateDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                })
                .ToListAsync();

            return Services;

        
        }
        public async Task<List<HairDresserReservationCreateDto>> LoadHairdressersForReservation()
            
        {

          

            List<HairDresserReservationCreateDto> hairDressers= await repository.All<HairDresser>()
                .Select(x => new HairDresserReservationCreateDto
                {
                    Name = x.Name,
                    Value = x.Id
                })
                .ToListAsync();

            return hairDressers;

        }
            


        public async Task<ReservationResult> CreateReservation(ReservationDtoImport reservationDto, List<int> serviceIds)
        {


            List<int> serviceIdsInDb = await repository.All<Service>().Select(x => x.Id).ToListAsync();
            List<int> HairDresserIdsInDb = await repository.All<HairDresser>().Select(x => x.Id).ToListAsync();

            var today = DateOnly.FromDateTime(DateTime.Today);
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);

            bool areServiceIdsValid =
                serviceIds.Any() &&
                serviceIds.All(id => serviceIdsInDb.Contains(id));
            bool isHairDresserIdIsValid = HairDresserIdsInDb.Contains(reservationDto.HairDresserId);
            bool isSlotAvailable =!await repository.All<Reservation>().AnyAsync(x => x.ReservationDate == reservationDto.ReservationDate && x.ReservationTime == reservationDto.ReservationTime && x.HairDresserId == reservationDto.HairDresserId);
            bool isDayOfWeekForReservationValid = !nonWorkingDays.Contains(reservationDto.ReservationDate.DayOfWeek);

            if (areServiceIdsValid  && isHairDresserIdIsValid && isSlotAvailable && isDayOfWeekForReservationValid)
            {

                if (reservationDto.ReservationDate > today ||
              (reservationDto.ReservationDate == today &&
               reservationDto.ReservationTime > currentTime))
                {

                    bool isSlotValid = validSlots.Contains(reservationDto.ReservationTime);

                    decimal CorrectTotalPricePrice =  await repository.All<Service>().Where(x => serviceIds.Contains(x.Id)).Select(x => x.Price).SumAsync();


                    if (isSlotValid)
                    {

                        List<Service> servicesForReservationDb = await repository.All<Service>().Where(x => serviceIds.Contains(x.Id)).ToListAsync();
                        HairDresser? hairDresser = await repository.All<HairDresser>().FirstOrDefaultAsync(x => x.Id == reservationDto.HairDresserId);

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

                        repository.Add<Reservation>(reservationForDb);

                        try
                        {
                            await repository.SaveChangesAsync();


                            result.Status = ReservationStatus.Success;
                            result.Reservation = reservationForDb;


                            return (result);


                        }

                        catch (Exception)
                        {

                            result.Status = ReservationStatus.DatabaseError;


                            return (result);
                        }






                    }

                }

            }

            result.Status = ReservationStatus.WrongInputDataError;


            return result;




        }

    }
}