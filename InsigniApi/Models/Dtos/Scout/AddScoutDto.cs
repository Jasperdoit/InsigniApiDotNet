using System.ComponentModel.DataAnnotations;

namespace InsigniApi.Models.Dtos.Scout
{
    public class AddScoutDto
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "Tennure must be between Year 1 and 4")]
        public int Tennure { get; set; }
        [Required]
        public Guid ScoutGroupId { get; set; }

    }
}
