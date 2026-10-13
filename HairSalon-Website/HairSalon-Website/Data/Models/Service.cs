using HairSalonWebsite.Common;
using HairSalonWebsite.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HairSalonWebsite.Common.ValidationConstrants.ServiceConstrants;


namespace HairSalonWebsite.Data.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Названието на услугата е задължително")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Полът е задължителен")]
        public Gender Gender { get; set; } 


        [Required(ErrorMessage = "Продължителността е задължителна")]
        [Range(DurationInMinutesMin, DurationInMinutesMax, ErrorMessage = "Продължителността трябва да бъде между {0} и {1} минути")]
        public int DurationInMinutes { get; set; }

        [Required(ErrorMessage = "Цената е задължителна")]
        [Range(typeof(decimal), nameof(PriceMin), nameof(PriceMax), ErrorMessage = "Цената трябва да бъде между {0} и {1} лв.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Снимката на услугата е задължителна")]
        [MaxLength(ServicePhotoExampleUrlMaxLength, ErrorMessage = "URL адресът на снимката не трябва да надвишава {0} знака")]
        public string ServicePhotoExampleUrl { get; set; } = null!;


        [MaxLength(DetailsMaxLength, ErrorMessage = "Описанието не трябва да надвишава {0} знака")]

        public string? Details { get; set; }

        public virtual ICollection<HairDresser> HairDressers { get; set; } = new HashSet<HairDresser>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();


    }
}
