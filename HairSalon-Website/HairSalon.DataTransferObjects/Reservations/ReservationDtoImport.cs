using System.ComponentModel.DataAnnotations;
using static HairSalon.Common.ValidationConstants.ReservationConstants;

namespace HairSalon.DataTransferObjects.Reservations
{
    public class ReservationDtoImport
    {
        [Required(ErrorMessage = "Името е задължително")]
        [MinLength(ClientNameMinLength, ErrorMessage = "Името трябва да съдържа поне {1} букви")]
        [MaxLength(ClientNameMaxLength, ErrorMessage = "Името не трябва да надвишава {1} знака")]
        [RegularExpression(@"^[А-Яа-яA-Za-z]+(?:[ -][А-Яа-яA-Za-z]+)*$",
            ErrorMessage = "Името трябва да съдържа само букви, интервали или тире")]
        public string ClientName { get; set; } = null!;

        [Required(ErrorMessage = "Телефонът е задължителен")]
        [MinLength(ClientPhoneMinLength, ErrorMessage = "Телефонът трябва да има поне {1} знака")]
        [MaxLength(ClientPhoneMaxLength, ErrorMessage = "Телефонът не трябва да надвишава {1} знака")]
        [RegularExpression(@"^\+?[0-9 ]+$", ErrorMessage = "Телефонът трябва да съдържа само цифри, интервали или водещ знак +")]
        public string ClientPhone { get; set; } = null!;

        [Required(ErrorMessage = "Датата на резервация е задължителна")]
        public DateOnly? ReservationDate { get; set; }

        [Required(ErrorMessage = "Часът на резервация е задължителен")]
        public TimeOnly? ReservationTime { get; set; }

        [Required(ErrorMessage = "Моля изберете фризьор!")]
        public int? HairDresserId { get; set; }
    }
}
