using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HairSalonWebsite.Common.ValidationConstrants.ReservationConstrants;

namespace HairSalonWebsite.Data.Models
{
    public class Reservation
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на клиента е задължително")]
        [MaxLength(ClientNameMaxLength, ErrorMessage = "Името на клиента не трябва да надвишава {0} знака")]
        public string ClientName { get; set; } = null!;

        [Required(ErrorMessage = "Телефонът е задължителен")]
        [MinLength(ClientPhoneMinLength, ErrorMessage = "Телефонът трябва да има поне {0} цифри")]
        [MaxLength(ClientPhoneMaxLength, ErrorMessage = "Телефонът не трябва да надвишава {0} цифри")]
        public string ClientPhone { get; set; } = null!;

        [Required(ErrorMessage = "Датата на резервация е задължителна")]
        public DateTime ReservationDate { get; set; }



        [ForeignKey(nameof(HairDresser))]
        [Required(ErrorMessage ="Моля изберете фризьор!")]
        public int HairDresserId { get; set; }

        public virtual HairDresser HairDresser { get; set; } = null!;

        public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();



    }
}
