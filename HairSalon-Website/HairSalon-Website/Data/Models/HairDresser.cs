using System.ComponentModel.DataAnnotations;
using static HairSalon_Website.Common.ValidationConstrants;

namespace HairSalon_Website.Data.Models
{
    public class HairDresser
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(HairDreeserConstrants.NameMaxLength, ErrorMessage = "Името не трябва да надвишава {0} знака")]
        public string Name { get; set; } = null!;


        public int? YearsOfExperiance { get; set; }

        [Required]
        public string AcademicCertificateUrl { get; set; } = null!;

        [Required]

        public string ProfilePictureUrl { get; set; } = null!;

        public string? AdditionalInformation { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();

    }
}
