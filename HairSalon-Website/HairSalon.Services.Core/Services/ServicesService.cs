using HairSalon.Data.Models;
using HairSalon.DataTransferObjects.Service;
using HairSalon.Services.Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HairSalon.Services.Core.Services
{
    public class ServicesService(IRepository repository) : IServicesService
    {
        public async Task<List<ServiceView>> GetAllServicesAsync()
        {
            List<ServiceView> services = await repository.All<Service>()
                .Select(x => new ServiceView { Name = x.Name, Details = x.Details, Price = x.Price, ServicePhotoExampleUrl = x.ServicePhotoExampleUrl })
                .ToListAsync();

            return services;
        }
    }
}
