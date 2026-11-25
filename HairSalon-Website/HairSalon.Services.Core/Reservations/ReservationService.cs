using HairSalon.Data;
using HairSalon.Data.Contracts;
using HairSalon.Data.DTOs.HairDresser;
using HairSalon.Data.DTOs.Reservations;
using HairSalon.Data.DTOs.Service;
using HairSalon.Data.Models;
using HairSalon.Services.Core.Contracts;
using HairSalon.Services.Core.Reservations.Enums;
using Microsoft.EntityFrameworkCore;
using HairSalon.Services.Core.Messeges;
using SalonMapper = HairSalon.Services.Core.Mapper.Mapper;



namespace HairSalon.Services.Core.Reservations
{
    public  class ReservationService(IRepository repository, ReservationResult result, SalonMapper mapper, IEmailService emailService) : IReservationService
    {
        private static readonly TimeZoneInfo SalonTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Sofia");
        private static DateTime SalonNow => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, SalonTimeZone);

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
        
            List<ServiceInReservationCreateDto> Services = await mapper
                .ProjectReservationServices(repository.All<Service>())
                .ToListAsync();

            return Services;

        
        }
        public async Task<List<HairDresserReservationCreateDto>> LoadHairdressersForReservation()
            
        {

          

            List<HairDresserReservationCreateDto> hairDressers= await mapper
                .ProjectReservationHairdressers(repository.All<HairDresser>())
                .ToListAsync();

            return hairDressers;

        }



       
        public async Task<ReservationResult> CreateReservation(ReservationDtoImport reservationDto, List<int> serviceIds)
        {


            List<int> serviceIdsInDb = await repository.All<Service>().Select(x => x.Id).ToListAsync();
            List<int> HairDresserIdsInDb = await repository.All<HairDresser>().Select(x => x.Id).ToListAsync();
            string messegeForClient = "";
            string messegeForHairdresser = "";
            Reservation reservationForDb = null;



            var now = SalonNow;
            var today = DateOnly.FromDateTime(now);
            var currentTime = TimeOnly.FromDateTime(now);

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

                         reservationForDb = new Reservation
                        {
                            ClientName = reservationDto.ClientName,
                            ClientPhone = reservationDto.ClientPhone,
                            ClientEmail = reservationDto.ClientEmail,
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

                          


                        }

                        catch (Exception)
                        {

                            result.Status = ReservationStatus.DatabaseError;


                            return (result);
                        }






                    }
                    else
                    {

                        result.Status = ReservationStatus.WrongInputDataError;
                        return result;
                    }

                }
                else
                {

                    result.Status = ReservationStatus.WrongInputDataError;
                    return result;
                }

            }
            else
            {
                result.Status = ReservationStatus.WrongInputDataError;


                return result;
            }


            try
            {
                if (reservationForDb.ClientEmail != null)
                {
                    messegeForClient = ReservationEmailMessages.GetClientMessage(reservationForDb);
                    await emailService.SendEmail(reservationForDb.ClientEmail, "Hair Salon Reservation Confirmation", messegeForClient);
                }


                messegeForHairdresser = ReservationEmailMessages.GetHairdresserMessage(reservationForDb);

                await emailService.SendEmail(reservationForDb.HairDresser.Email, "Graphite Barber Studio - New Reservation", messegeForHairdresser);


                return result;
            }
            catch(Exception)
            {
                result.Status = ReservationStatus.EmailSendingError;
                return result;
            }



        }


        public async Task<List<HairdresserViewModel>> ReadAllHairDressersWithTheirReservationsCountPerDayOfTheMounth()
        {
            await RemoveOldReservations();



            var reservationsPerHairdresser = await repository.All<HairDresser>()
                .Select(x => new 
                {
                    Id = x.Id,
                    Name = x.Name,
                    ReservationDates = x.Reservations.GroupBy(rD => rD.ReservationDate).Select(group => new {Date=group.Key, Count=group.Count()}).ToList()
                }).ToListAsync();





            List<HairdresserViewModel> reservationsPerDayOfTheMounth = reservationsPerHairdresser.Select(x => new HairdresserViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ReservationsCountPerDay = x.ReservationDates.ToDictionary(x => x.Date, x => x.Count)
            }).ToList();

            return reservationsPerDayOfTheMounth;

        }




        public async Task RemoveOldReservations()
        {

            DateOnly todayDate = DateOnly.FromDateTime(SalonNow);

            DateOnly theDate30DaysBeforeToday = todayDate.AddDays(-30);


            
                await repository.All<Reservation>()
                .Where(x => x.ReservationDate < theDate30DaysBeforeToday)
                .ExecuteDeleteAsync();
            


                
        }
        public async Task<ReservationDtoImport?> FindReservationWithIdForEdit(int id)
        {

            Reservation? reservation = await repository.All<Reservation>().FirstOrDefaultAsync(x => x.Id == id);
            if (reservation is null)
            {
                return null;
            }

            return mapper.ToReservationDtoImport(reservation);

        }
        public async Task<List<int>> GetServicesIdsForReservation(int reservationId)
        {
            Reservation reservation = await repository.All<Reservation>().Include(x => x.Services).FirstOrDefaultAsync(x => x.Id == reservationId);

            List<int> serviceIds = new List<int>();

            if(reservation!=null)
            {
                serviceIds = reservation.Services.Select(x => x.Id).ToList();

            }

            return serviceIds;
        }

        

      public async Task<ReservationResult> EditReservation(int id, ReservationDtoImport reservationDto, List<int> serviceIds)


        {


            List<int> serviceIdsInDb = await repository.All<Service>().Select(x => x.Id).ToListAsync();
            List<int> HairDresserIdsInDb = await repository.All<HairDresser>().Select(x => x.Id).ToListAsync();

            var now = SalonNow;
            var today = DateOnly.FromDateTime(now);
            var currentTime = TimeOnly.FromDateTime(now);
            Reservation reservationForDb = null;
            string messegeForClient = "";
            string messegeForHairdresser = "";


            bool areServiceIdsValid =
                serviceIds.Any() &&
                serviceIds.All(id => serviceIdsInDb.Contains(id));
            bool isHairDresserIdIsValid = HairDresserIdsInDb.Contains(reservationDto.HairDresserId);
            bool isSlotAvailable = !await repository.All<Reservation>().AnyAsync(x => (x.ReservationDate == reservationDto.ReservationDate && x.ReservationTime == reservationDto.ReservationTime && x.HairDresserId == reservationDto.HairDresserId)&& x.Id!=id);
            bool isDayOfWeekForReservationValid = !nonWorkingDays.Contains(reservationDto.ReservationDate.DayOfWeek);
            bool DoesReservationIdForEditValid = await repository.All<Reservation>().AnyAsync(x => x.Id == id);

            if (areServiceIdsValid && isHairDresserIdIsValid && isSlotAvailable && isDayOfWeekForReservationValid&& DoesReservationIdForEditValid)
            {

                if (reservationDto.ReservationDate > today ||
              (reservationDto.ReservationDate == today &&
               reservationDto.ReservationTime > currentTime))
                {

                    bool isSlotValid = validSlots.Contains(reservationDto.ReservationTime);

                    decimal CorrectTotalPricePrice = await repository.All<Service>().Where(x => serviceIds.Contains(x.Id)).Select(x => x.Price).SumAsync();


                    if (isSlotValid)
                    {

                        List<Service> servicesForReservationDb = await repository.All<Service>().Where(x => serviceIds.Contains(x.Id)).ToListAsync();
                        HairDresser? hairDresser = await repository.All<HairDresser>().FirstOrDefaultAsync(x => x.Id == reservationDto.HairDresserId);

                         reservationForDb = await repository.All<Reservation>()
                            .Include(x => x.Services)
                            .FirstOrDefaultAsync(x => x.Id == id);

                        if (reservationForDb is null)
                        {
                            result.Status = ReservationStatus.WrongInputDataError;
                            return result;
                        }


                        reservationForDb.ClientName = reservationDto.ClientName;
                        reservationForDb.ClientPhone = reservationDto.ClientPhone;
                        reservationForDb.ReservationDate = reservationDto.ReservationDate;
                        reservationForDb.ReservationTime = reservationDto.ReservationTime;
                        reservationForDb.HairDresser = hairDresser;
                        reservationForDb.TotalPrice = CorrectTotalPricePrice;
                        reservationForDb.Services = servicesForReservationDb;

                        
                        
                            reservationForDb.ClientEmail = reservationDto.ClientEmail;






                        try
                        {
                            await repository.SaveChangesAsync();


                            result.Status = ReservationStatus.Success;
                            result.Reservation = reservationForDb;




                        }

                        catch (Exception)
                        {

                            result.Status = ReservationStatus.DatabaseError;


                            return (result);
                        }






                    }
                    else
                    {

                        result.Status = ReservationStatus.WrongInputDataError;
                        return result;
                    }

                }
                else
                {

                    result.Status = ReservationStatus.WrongInputDataError;
                    return result;
                }

            }
            else
            {
                result.Status = ReservationStatus.WrongInputDataError;


                return result;
            }


            try
            {
                if (reservationForDb.ClientEmail != null)
                {
                    messegeForClient = ReservationEmailMessages.GetClientMessage(reservationForDb);
                    await emailService.SendEmail(reservationForDb.ClientEmail, "Hair Salon Reservation Confirmation", messegeForClient);
                }


                messegeForHairdresser = ReservationEmailMessages.GetHairdresserMessage(reservationForDb);

                await emailService.SendEmail(reservationForDb.HairDresser.Email, "Graphite Barber Studio - New Reservation", messegeForHairdresser);


                return result;
            }
            catch (Exception)
            {
                result.Status = ReservationStatus.EmailSendingError;
                return result;
            }




        }

        public async Task<List<ReservationViewModel>> GetHairdresserReservations(int HairDresserId, DateOnly date)
        {


            List<ReservationViewModel> hairdresserReservations = await mapper
                .ProjectReservationViews(repository.All<Reservation>()
                    .Where(x => x.HairDresserId == HairDresserId)
                    .Where(r => r.ReservationDate == date))
                .ToListAsync();



            return hairdresserReservations;







        }

        public async Task<List<EmployeeReservationViewModel>> GetEmployeeReservations(int HairDresserId, DateOnly date)
        {


            List<EmployeeReservationViewModel> hairdresserReservations = await mapper
                .ProjectEmployeeReservationViews(repository.All<Reservation>()
                    .Where(x => x.HairDresserId == HairDresserId)
                    .Where(r => r.ReservationDate == date))
                .ToListAsync();



            return hairdresserReservations;







        }

        public async Task<bool> Delete(int reservationId)
        {


            Reservation? reservation = await repository.All<Reservation>().FirstOrDefaultAsync(r => r.Id == reservationId);


            if(reservation==null)
            {
                return false;
            }

            repository.Remove<Reservation>(reservation);

            try
            {
                await repository.SaveChangesAsync();
                return true;

            }

            catch(Exception ex)
            {
                return false;

            }


        }




    }
}
