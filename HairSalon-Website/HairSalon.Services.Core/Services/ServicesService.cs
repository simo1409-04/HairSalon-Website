using HairSalon.Data.Contracts;
using HairSalon.Services.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using HairSalon.Data.Models;
using HairSalon.Data.DTOs.Service;

namespace HairSalon.Services.Core.Services
{
    public class ServicesService(IRepository repository) : IServicesService
    {
        public async Task<List<ServiceView>> GetAllServices()
        {
            List<ServiceView> services = await repository.All<Service>()
                .Select(x => new ServiceView { Id = x.Id, Name = x.Name, Details = x.Details, Price = x.Price })
                .ToListAsync();

            return services;
        }
        public async Task CreateService(ServiceView service)
        {
            Service serviceForDb = new Service { Details=service.Details, Name=service.Name, Price= service.Price };

            repository.Add<Service>(serviceForDb);

            await repository.SaveChangesAsync();

        }

        public async Task<ServiceView> EditService(int id)
        {
            Service serviceForEdit = await repository.All<Service>().FirstOrDefaultAsync(x => x.Id == id);

            if(serviceForEdit==null)
            {
                return null;

            }

            ServiceView serviceModel = new ServiceView { Id = serviceForEdit.Id, Details = serviceForEdit.Details, Name = serviceForEdit.Name, Price = serviceForEdit.Price };


            return serviceModel;

        }

        public async Task<Service> EditAndSaveService(int id, ServiceView model)
        {
            Service ServiceForEditInDb = await repository.All<Service>().FirstOrDefaultAsync(x => x.Id == id);

            if(ServiceForEditInDb==null)
            {

                return null;

            }
            ServiceForEditInDb.Details = model.Details;
            ServiceForEditInDb.Name = model.Name;
            ServiceForEditInDb.Price = model.Price;



            await repository.SaveChangesAsync();

            return ServiceForEditInDb;


        }
    }
}
