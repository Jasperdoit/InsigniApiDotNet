using System.ComponentModel.DataAnnotations;
using InsigniApi.Models.Attributes;

namespace InsigniApi.Models.Dtos.Insignia
{
    public class AddInsigniaDto
    {
        [Required]
        [Length(1, 100)]
        public required String Name { get; set; }
        [Required]
        [Url]
        [ImageUrl]
        public required String ImageUrl { get; set; }
    }
}
