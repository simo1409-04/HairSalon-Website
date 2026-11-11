using HairSalon.Data;
using HairSalon.Data.DTOs.HairDresser;
using HairSalon.Data.Models;
using HairSalon.Services.Core.Hairdressers.Enums;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HairSalon.Services.Core.Contracts
{
    public interface IHairdresserService
    {

        public Task<List<HairdresserDto>> ReadAll();
        public Task<HairDresserStatus> AddHairDresser(HairDresserInputModel model);

        public Task<HairdresserEdit> FindHairdresserById(int id);

        public Task<EditStatus> EditHairdresserById(int id, HairdresserEdit model);

        public  Task<DeleteStatus> Delete(int id);


    }
}
