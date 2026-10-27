using HairSalon.Data.Models;
using HairSalon.DataTransferObjects.HairDresser;
using HairSalon.DataTransferObjects.Reservations;
using HairSalon.DataTransferObjects.Service;
using HairSalon.Services.Core.Contracts;
using HairSalon.Services.Core.Reservations.Enums;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace HairSalon.Services.Core.Reservations
{
    public class ReservationService(IRepository repository) : IReservationService
    {
        private static readonly HashSet<TimeOnly> ValidSlots = new()
        {
            new(10, 0), new(10, 50), new(11, 40), new(12, 30),
            new(14, 0), new(14, 50), new(15, 40), new(16, 30),
            new(17, 20), new(18, 10), new(19, 0)
        };

        private static readonly HashSet<DayOfWeek> NonWorkingDays = new()
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday
        };

        public async Task<Dictionary<DateOnly, TimeOnly[]>> GetHairdresserBusySlots(int id)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var slots = await repository.All<Reservation>()
                .Where(x => x.HairDresserId == id && x.ReservationDate >= today)
                .Select(x => new { x.ReservationDate, x.ReservationTime })
                .ToListAsync();

            return slots.GroupBy(x => x.ReservationDate)
                .ToDictionary(group => group.Key, group => group.Select(x => x.ReservationTime).ToArray());
        }

        public async Task<List<ServiceInReservationCreateDto>> LoadServicesForReservation()
        {
            return await repository.All<Service>()
                .Select(x => new ServiceInReservationCreateDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                })
                .ToListAsync();
        }

        public async Task<List<HairDresserReservationCreateDto>> LoadHairdressersForReservation()
        {
            return await repository.All<HairDresser>()
                .Select(x => new HairDresserReservationCreateDto
                {
                    Name = x.Name,
                    Value = x.Id
                })
                .ToListAsync();
        }

        public async Task<Reservation?> GetReservationById(int id)
        {
            return await repository.All<Reservation>()
                .Include(x => x.HairDresser)
                .Include(x => x.Services)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ReservationResult> CreateReservation(ReservationDtoImport reservationDto, List<int> serviceIds)
        {
            var now = DateTime.Now;
            var today = DateOnly.FromDateTime(now);
            var currentTime = TimeOnly.FromDateTime(now);

            if (reservationDto.ReservationDate is not DateOnly date)
            {
                return Invalid(nameof(reservationDto.ReservationDate), "Избери дата за резервацията.");
            }

            if (reservationDto.ReservationTime is not TimeOnly time)
            {
                return Invalid(nameof(reservationDto.ReservationTime), "Избери час за резервацията.");
            }

            if (date < today || (date == today && time <= currentTime))
            {
                return Invalid(nameof(reservationDto.ReservationDate), "Избери бъдеща дата и час.");
            }

            if (NonWorkingDays.Contains(date.DayOfWeek))
            {
                return Invalid(nameof(reservationDto.ReservationDate), "Избраният ден е почивен.");
            }

            if (!ValidSlots.Contains(time))
            {
                return Invalid(nameof(reservationDto.ReservationTime), "Избери час от свободните часове в календара.");
            }

            if (reservationDto.HairDresserId is not int hairDresserId)
            {
                return Invalid(nameof(reservationDto.HairDresserId), "Избери фризьор.");
            }

            if (serviceIds == null || serviceIds.Count == 0)
            {
                return Invalid(string.Empty, "Избери поне една услуга.");
            }

            try
            {
                var hairDresser = await repository.All<HairDresser>()
                    .FirstOrDefaultAsync(x => x.Id == hairDresserId);

                if (hairDresser == null)
                {
                    return Invalid(nameof(reservationDto.HairDresserId), "Избери валиден фризьор.");
                }

                var selectedIds = serviceIds.Distinct().ToList();
                var services = await repository.All<Service>()
                    .Where(x => selectedIds.Contains(x.Id))
                    .ToListAsync();

                if (services.Count != selectedIds.Count)
                {
                    return Invalid(string.Empty, "Избери валидни услуги.");
                }

                bool isBusy = await repository.All<Reservation>().AnyAsync(x =>
                    x.HairDresserId == hairDresser.Id &&
                    x.ReservationDate == date &&
                    x.ReservationTime == time);

                if (isBusy)
                {
                    return Invalid(nameof(reservationDto.ReservationTime), "Този час вече е зает. Избери друг.");
                }

                var reservation = new Reservation
                {
                    ClientName = reservationDto.ClientName,
                    ClientPhone = reservationDto.ClientPhone,
                    ReservationDate = date,
                    ReservationTime = time,
                    HairDresser = hairDresser,
                    TotalPrice = services.Sum(x => x.Price),
                    Services = services
                };

                repository.Add(reservation);
                await repository.SaveChangesAsync();

                return new ReservationResult
                {
                    Status = ReservationStatus.Success,
                    Reservation = reservation
                };
            }
            catch (DbUpdateException)
            {
                return new ReservationResult { Status = ReservationStatus.DatabaseError };
            }
            catch (DbException)
            {
                return new ReservationResult { Status = ReservationStatus.DatabaseError };
            }
            catch (InvalidOperationException exception) when (exception.InnerException is DbException)
            {
                return new ReservationResult { Status = ReservationStatus.DatabaseError };
            }
        }

        private static ReservationResult Invalid(string field, string message)
        {
            return new ReservationResult
            {
                Status = ReservationStatus.WrongInputDataError,
                ErrorField = field,
                ErrorMessage = message
            };
        }
    }
}
