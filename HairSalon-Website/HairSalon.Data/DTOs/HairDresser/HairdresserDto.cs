using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static HairSalon.Data.Common.ValidationConstants;

namespace HairSalon.Data.DTOs.HairDresser
{
    public class HairdresserDto
    {

        public int Id { get; set; }

        
        public string Name { get; set; } = null!;



        public string AcademicCertificateUrl { get; set; } = null!;


        public string ProfilePictureUrl { get; set; } = null!;

        public string? AdditionalInformation { get; set; }


    }
}
