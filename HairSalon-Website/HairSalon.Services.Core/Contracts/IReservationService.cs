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
        public Task<List<HairdresserViewModel>> ReadAllHairDressersWithTheirReservations();
        public Task<ReservationDtoImport> FindReservationWithIdForEdit(int id);


        public Task<List<int>> GetServicesIdsForReservation(int reservationId);
        public Task<ReservationResult> EditReservation(int id, ReservationDtoImport reservationDto, List<int> serviceIds);


    }
}
