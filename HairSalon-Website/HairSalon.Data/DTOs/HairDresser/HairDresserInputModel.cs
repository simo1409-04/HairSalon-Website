using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using static HairSalon.Data.Common.ValidationConstants;

namespace HairSalon.Data.DTOs.HairDresser
{
    public class HairDresserInputModel
    {



        [Required(ErrorMessage = "Името е задължително")]
        [MaxLength(HairDresserConstants.NameMaxLength,
            ErrorMessage = "Името не трябва да надвишава {1} знака")]
        [RegularExpression(@"^[А-Яа-яЁёЪъЬьІіҺһ\s\-”„""']+$", ErrorMessage = "Името трябва да съдържа само букви на кирилица, интервали или тирета.")] public string Name { get; set; } = null!;



        [Required(ErrorMessage = "Снимката на сертификата е задължителна")]
        public IFormFile AcademicCertificate { get; set; } = null!;




        [Required(ErrorMessage = "Профилната снимка е задължителна")]
        public IFormFile ProfilePicture { get; set; } = null!;


        [MaxLength(HairDresserConstants.DetailsMaxLength)]
        public string? AdditionalInformation { get; set; }

    } 

}

