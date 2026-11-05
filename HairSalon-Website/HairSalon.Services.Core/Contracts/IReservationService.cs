using HairSalon.Data.DTOs.HairDresser;
using HairSalon.Data.DTOs.Reservations;
using HairSalon.Data.DTOs.Service;
using HairSalon.Services.Core.Reservations;

namespace HairSalon.Services.Core.Contracts
{
    public interface IReservationService
    {
        Task<Dictionary<DateOnly, TimeOnly[]>> GetHairdresserBusySlots(int id);
        Task<List<ServiceInReservationCreateDto>> LoadServicesForReservation();
        Task<List<HairDresserReservationCreateDto>> LoadHairdressersForReservation();
        Task<ReservationResult> CreateReservation(ReservationDtoImport reservationDto, List<int> serviceIds);
    }
}
