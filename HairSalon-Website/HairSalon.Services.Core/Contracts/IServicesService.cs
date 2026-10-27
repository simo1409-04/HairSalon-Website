using HairSalon.Data.Models;
using HairSalon.DataTransferObjects.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace HairSalon.Services.Core.Contracts
{
    public interface IServicesService
    {



        public List<ServiceView> GetAllServices();
        public Service GetService(int serviceId);

    }
}
