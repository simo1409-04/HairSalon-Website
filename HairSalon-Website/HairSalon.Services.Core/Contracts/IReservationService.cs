using HairSalon.Data.Models;
using HairSalon.DataTransferObjects.HairDresser;
using HairSalon.DataTransferObjects.Reservations;
using HairSalon.DataTransferObjects.Service;
using HairSalon.Services.Core.Reservations;

namespace HairSalon.Services.Core.Contracts
{
    public interface IReservationService
    {
        Task<Dictionary<DateOnly, TimeOnly[]>> GetHairdresserBusySlots(int id);
        Task<List<ServiceInReservationCreateDto>> LoadServicesForReservation();
        Task<List<HairDresserReservationCreateDto>> LoadHairdressersForReservation();
        Task<Reservation?> GetReservationById(int id);
        Task<ReservationResult> CreateReservation(ReservationDtoImport reservationDto, List<int> serviceIds);
    }
}
