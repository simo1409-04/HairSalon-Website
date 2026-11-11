using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using static HairSalon.Data.Common.ValidationConstants;

namespace HairSalon.Data.DTOs.HairDresser
{
    public class HairdresserEdit
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Името е задължително")]
        [MaxLength(HairDresserConstants.NameMaxLength,
            ErrorMessage = "Името не трябва да надвишава {1} знака")]
        [RegularExpression(@"^[А-Яа-яЁёЪъЬьІіҺһ\s\-”„""']+$", ErrorMessage = "Името трябва да съдържа само букви на кирилица, интервали или тирета.")] public string Name { get; set; } = null!;



        public IFormFile? AcademicCertificate { get; set; } = null!;




        public IFormFile? ProfilePicture { get; set; } = null!;


        [Required]
        
        [MaxLength(HairDresserConstants.DetailsMaxLength)]

        public string AdditionalInformation { get; set; }

    } 

}

