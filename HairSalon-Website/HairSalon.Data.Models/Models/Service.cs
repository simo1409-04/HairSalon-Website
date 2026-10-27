using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HairSalon.Common.ValidationConstants.ServiceConstants;


namespace HairSalon.Data.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Названието на услугата е задължително")]
        public string Name { get; set; } = null!;




       
        [Required(ErrorMessage = "Цената е задължителна")]
        [Range(typeof(decimal), PriceMin, PriceMax, ErrorMessage = "Цената трябва да бъде между {1} и {2} евро")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Снимката на услугата е задължителна")]
        [MaxLength(ServicePhotoExampleUrlMaxLength, ErrorMessage = "URL адресът на снимката не трябва да надвишава {1} знака")]
        public string ServicePhotoExampleUrl { get; set; } = null!;


        [MaxLength(DetailsMaxLength, ErrorMessage = "Описанието не трябва да надвишава {1} знака")]

        public string? Details { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();


    }
}
