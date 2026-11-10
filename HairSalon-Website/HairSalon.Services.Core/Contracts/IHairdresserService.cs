using HairSalon.Data.DTOs.HairDresser;
using HairSalon.Services.Core.Hairdressers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HairSalon.Services.Core.Contracts
{
    public interface IHairdresserService
    {

        public Task<List<HairdresserDto>> ReadAll();
        public Task<HairDresserStatus> AddHairDresser(HairDresserInputModel model);


    }
}
