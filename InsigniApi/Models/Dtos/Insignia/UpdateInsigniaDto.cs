using InsigniApi.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace InsigniApi.Models.Dtos.Insignia
{
    public class UpdateInsigniaDto
    {
        [Required]
        [Length(1, 100)]
        public required String Name { get; set; }
        [Required]
        [Url]
        [ImageUrl]
        public required String ImageUrl { get; set; }
        [Required]
        [Range(0, 100)]
        public int RequiredAssignments { get; set; }
    }
}
