using HairSalon.Data.DTOs.Service;
using HairSalon.Data.Models;

namespace HairSalon.Services.Core.Contracts
{
    public interface IServicesService
    {
       public Task<List<ServiceView>> GetAllServices();
        public  Task CreateService(ServiceView service);

        public Task<ServiceView> EditService(int id);

        public Task<Service> EditAndSaveService(int id, ServiceView model);
        public  Task<bool> DeleteService(int id);


    }
}
