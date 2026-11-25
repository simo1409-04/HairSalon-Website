using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HairSalon.Data.Common.ValidationConstants;
using static HairSalon.Data.Common.ValidationConstants.ReservationConstants;

namespace HairSalon.Data.Models
{
    public class Reservation
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [MaxLength(ClientNameMaxLength, ErrorMessage = "Името не трябва да надвишава 100 знака")]
        [RegularExpression(
    @"^[\p{L}]+(?:[ '\-][\p{L}]+)*$",
    ErrorMessage = "Името може да съдържа само букви, интервали, тирета и апострофи.")]
        public string ClientName { get; set; } = null!;



        [Required(ErrorMessage = "Телефонът е задължителен")]
        [MinLength(ClientPhoneMinLength, ErrorMessage = "Телефонът трябва да има поне {1} цифри")]
        [MaxLength(ClientPhoneMaxLength, ErrorMessage = "Телефонът не трябва да надвишава {1} цифри")]
        [RegularExpression(ClientPhonePattern, ErrorMessage = "Телефонът трябва да съдържа само цифри")]
        public string ClientPhone { get; set; } = null!;

        [Required(ErrorMessage = "Датата на резервация е задължителна")]
        public DateOnly ReservationDate { get; set; }



        [Required(ErrorMessage = "Часът на резервация е задължителен")]
        public TimeOnly ReservationTime { get; set; }

        [ForeignKey(nameof(HairDresser))]
        [Required(ErrorMessage ="Моля изберете фризьор!")]
        public int HairDresserId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [EmailAddress(ErrorMessage = "Въведи валиден имейл адрес")]
        [RegularExpression(HairDresserConstants.EmailPattern, ErrorMessage = "Въведи валиден имейл адрес")]
        [MaxLength(HairDresserConstants.EmailMaxLength,
            ErrorMessage = "Имейл адресът не трябва да надвишава {1} знака")]
        public string? ClientEmail { get; set; }

        public virtual HairDresser? HairDresser { get; set; }

        public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();



    }
}
