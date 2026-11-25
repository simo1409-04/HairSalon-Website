using HairSalon.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HairSalon.Data.Common.ValidationConstants;
using static HairSalon.Data.Common.ValidationConstants.ReservationConstants;

namespace HairSalon.Data.DTOs.Reservations
{
    public class ReservationDtoImport
    {

        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Името трябва да съдържа поне {1} знака")]
        [MaxLength(ClientNameMaxLength, ErrorMessage = "Името не трябва да надвишава {1} знака")]
        [RegularExpression(@"^[А-Яа-яA-Za-z]+(?: +[А-Яа-яA-Za-z]+)*$", ErrorMessage = "Името може да съдържа само букви и интервали между думите")]
        [Required(ErrorMessage = "Името е задължително")]
        public string ClientName { get; set; } = null!;

        [Required(ErrorMessage = "Телефонът е задължителен")]
        [MinLength(ClientPhoneMinLength, ErrorMessage = "Телефонът трябва да има поне {1} цифри")]
        [MaxLength(ClientPhoneMaxLength, ErrorMessage = "Телефонът не трябва да надвишава {1} цифри")]
        [RegularExpression(ClientPhonePattern, ErrorMessage = "Телефонът трябва да съдържа само цифри")]
        public string ClientPhone { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Въведи валиден имейл адрес")]
        [RegularExpression(HairDresserConstants.EmailPattern, ErrorMessage = "Въведи валиден имейл адрес")]
        [MaxLength(HairDresserConstants.EmailMaxLength,
            ErrorMessage = "Имейл адресът не трябва да надвишава {1} знака")]
        public string? ClientEmail { get; set; }

        [Required(ErrorMessage = "Датата на резервация е задължителна")]
        public DateOnly ReservationDate { get; set; }

        [Required(ErrorMessage = "Часът на резервация е задължителен")]
        public TimeOnly ReservationTime { get; set; }

        [ForeignKey(nameof(HairDresser))]
        [Required(ErrorMessage = "Моля изберете фризьор!")]
        public int HairDresserId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
