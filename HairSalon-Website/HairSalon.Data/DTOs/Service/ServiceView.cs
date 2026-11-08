using HairSalon.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static HairSalon.Data.Common.ValidationConstants.ServiceConstants;


namespace HairSalon.Data.DTOs.Service
{
    public class ServiceView
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Названието на услугата е задължително")]
        public string Name { get; set; } = null!;





        [Required(ErrorMessage = "Цената е задължителна")]
        [Range(typeof(decimal), "0.01", "10000.00", ParseLimitsInInvariantCulture = true, ErrorMessage = "Цената трябва да бъде между {1} и {2} евро.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }




        [MaxLength(DetailsMaxLength, ErrorMessage = "Описанието не трябва да надвишава {1} знака")]

        public string? Details { get; set; }



    }
    
}
