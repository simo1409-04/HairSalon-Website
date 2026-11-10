using System.ComponentModel.DataAnnotations;
using static HairSalon.Data.Common.ValidationConstants;

namespace HairSalon.Data.Models
{
    public class HairDresser
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(HairDresserConstants.NameMaxLength, ErrorMessage = "Името не трябва да надвишава {1} знака")]

        public string Name { get; set; } = null!;



        [Required]
        [MaxLength(HairDresserConstants.AcademicCertificateUrlMaxLength)]

        public string AcademicCertificateUrl { get; set; } = null!;

        [Required]
        [MaxLength(HairDresserConstants.ProfilePictureUrlMaxLength)]

        public string ProfilePictureUrl { get; set; } = null!;

        [MaxLength(HairDresserConstants.DetailsMaxLength)]
        [Required]

        public string AdditionalInformation { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

    }
}
