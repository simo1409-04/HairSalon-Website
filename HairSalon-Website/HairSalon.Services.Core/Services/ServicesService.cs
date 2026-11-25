using HairSalon.Data.Contracts;
using HairSalon.Data.DTOs.Service;
using HairSalon.Data.Models;
using HairSalon.Services.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using SalonMapper = HairSalon.Services.Core.Mapper.Mapper;

namespace HairSalon.Services.Core.Services
{
    public class ServicesService(IRepository repository, SalonMapper mapper) : IServicesService
    {
        public async Task<List<ServiceView>> GetAllServices()
        {
            List<ServiceView> services = await mapper.ProjectServiceViews(repository.All<Service>())
                .ToListAsync();

            return services;
        }
        public async Task CreateService(ServiceView service)
        {
            Service serviceForDb = mapper.ToNewService(service);

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

            ServiceView serviceModel = mapper.ToServiceView(serviceForEdit);


            return serviceModel;

        }

        public async Task<Service> EditAndSaveService(int id, ServiceView model)
        {
            Service ServiceForEditInDb = await repository.All<Service>().FirstOrDefaultAsync(x => x.Id == id);

            if(ServiceForEditInDb==null)
            {

                return null;

            }
            mapper.UpdateService(model, ServiceForEditInDb);



            await repository.SaveChangesAsync();

            return ServiceForEditInDb;


        }

        public async Task<bool> DeleteService(int id)
        {
            Service? ServiceForEditInDb = await repository.All<Service>().FirstOrDefaultAsync(x => x.Id == id);

            bool isDeleted = false;

            if(ServiceForEditInDb==null)
            {

                return isDeleted;

            }

            try
            {
                repository.Remove<Service>(ServiceForEditInDb);

                await repository.SaveChangesAsync();
                isDeleted = true;
                return isDeleted;

            }




            catch (Exception ex)
            {
                return isDeleted;

            }



        }
    }
}
