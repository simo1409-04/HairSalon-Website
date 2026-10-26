using HairSalon.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HairSalon.Common.ValidationConstrants.ReservationConstrants;

namespace HairSalon.DataTransferObjects.DTOs_Import
{
    public class ReservationDtoImport
    {


        [MinLength(3,
            ErrorMessage = "Името трябва да съдържа поне {1} букви")]

        [MaxLength(ClientNameMaxLength,
            ErrorMessage = "Името не трябва да надвишава {1} знака")]

        [RegularExpression(@"^[А-Яа-яA-Za-z]+$",
            ErrorMessage = "Името трябва да съдържа само букви")]

        public string ClientName { get; set; } = null!;


        [Required(ErrorMessage = "Телефонът е задължителен")]
        [MinLength(ClientPhoneMinLength, ErrorMessage = "Телефонът трябва да има поне {1} цифри")]
        [MaxLength(ClientPhoneMaxLength, ErrorMessage = "Телефонът не трябва да надвишава {1} цифри")]
        public string ClientPhone { get; set; } = null!;

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
