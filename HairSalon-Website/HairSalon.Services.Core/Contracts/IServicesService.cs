using HairSalon.Data.DTOs.Service;

namespace HairSalon.Services.Core.Contracts
{
    public interface IServicesService
    {
        Task<List<ServiceView>> GetAllServicesAsync();
    }
}
