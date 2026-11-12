using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HairSalon.Data.DTOs.Gallery
{
    public class GalleryDto
    {
        [Required(ErrorMessage = "Избери снимка за качване.")]
        public IFormFile photo { get; set; } = default!;
    }
}
