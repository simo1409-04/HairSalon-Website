using HairSalon.Data.Contracts;
using HairSalon.Services.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using HairSalon.Data.Models;
using HairSalon.Data.DTOs.Service;

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
