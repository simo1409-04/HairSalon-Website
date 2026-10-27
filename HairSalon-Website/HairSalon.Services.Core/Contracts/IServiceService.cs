using HairSalon.DataTransferObjects.Service;

namespace HairSalon.Services.Core.Contracts
{
    public interface IServiceService
    {
        Task<List<ServiceView>> GetAllServicesAsync();
    }
}
