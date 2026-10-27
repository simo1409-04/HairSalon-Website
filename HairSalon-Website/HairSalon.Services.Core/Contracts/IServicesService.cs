using HairSalon.DataTransferObjects.Service;

namespace HairSalon.Services.Core.Contracts
{
    public interface IServicesService
    {
        Task<List<ServiceView>> GetAllServicesAsync();
    }
}
