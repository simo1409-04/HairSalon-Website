using HairSalon.Data.Models;
using HairSalon.Services.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using HairSalon.DataTransferObjects.Service;

namespace HairSalon.Services.Core
{
    public class ServicesService(IRepository repository):IServicesService
    {


        public List<ServiceView> GetAllServices()
        {

            List<ServiceView> services = repository.All<ServiceView>().ToList();

            return services;

        }
        public Service GetService(int serviceId)
        {
            Service ser = new Service();
            return ser;
        }

    }
}
