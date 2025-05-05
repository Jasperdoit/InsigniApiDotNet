using System.ComponentModel.DataAnnotations;

namespace InsigniApi.Models.Dtos.ScoutGroup
{
    public class AddScoutGroupDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
