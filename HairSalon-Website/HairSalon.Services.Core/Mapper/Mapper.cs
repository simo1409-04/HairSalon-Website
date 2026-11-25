using System;
using HairSalon.Data.DTOs.HairDresser;
using HairSalon.Data.DTOs.Reservations;
using HairSalon.Data.DTOs.Service;
using HairSalon.Data.Models;
using Riok.Mapperly.Abstractions;

namespace HairSalon.Services.Core.Mapper
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class Mapper
    {
        public partial IQueryable<ServiceView> ProjectServiceViews(IQueryable<Service> services);
        public partial ServiceView ToServiceView(Service service);

        [MapperIgnoreTarget(nameof(Service.Id))]
        [MapperIgnoreTarget(nameof(Service.Reservations))]
        public partial Service ToNewService(ServiceView service);

        [MapperIgnoreTarget(nameof(Service.Id))]
        [MapperIgnoreTarget(nameof(Service.Reservations))]
        public partial void UpdateService(ServiceView source, Service target);

        public partial IQueryable<HairdresserDto> ProjectHairdresserDtos(IQueryable<HairDresser> hairdressers);
        public partial IQueryable<HairdresserEdit> ProjectHairdresserEdits(IQueryable<HairDresser> hairdressers);

        [MapperIgnoreTarget(nameof(HairdresserEdit.AcademicCertificate))]
        [MapperIgnoreTarget(nameof(HairdresserEdit.ProfilePicture))]
        private partial HairdresserEdit ToHairdresserEdit(HairDresser hairdresser);

        public partial IQueryable<ServiceInReservationCreateDto> ProjectReservationServices(IQueryable<Service> services);
        public partial IQueryable<HairDresserReservationCreateDto> ProjectReservationHairdressers(IQueryable<HairDresser> hairdressers);

        [MapProperty(nameof(HairDresser.Id), nameof(HairDresserReservationCreateDto.Value))]
        private partial HairDresserReservationCreateDto ToReservationHairdresser(HairDresser hairdresser);

        public partial ReservationDtoImport ToReservationDtoImport(Reservation reservation);
        public partial IQueryable<ReservationViewModel> ProjectReservationViews(IQueryable<Reservation> reservations);

        [MapperIgnoreTarget(nameof(ReservationViewModel.HairDresserId))]
        private partial ReservationViewModel ToReservationView(Reservation reservation);

        public partial IQueryable<EmployeeReservationViewModel> ProjectEmployeeReservationViews(IQueryable<Reservation> reservations);

        [MapperIgnoreTarget(nameof(EmployeeReservationViewModel.HairDresserId))]
        private partial EmployeeReservationViewModel ToEmployeeReservationView(Reservation reservation);

        private partial ServiceViewModelForReservationView ToReservationServiceView(Service service);
    }
}
