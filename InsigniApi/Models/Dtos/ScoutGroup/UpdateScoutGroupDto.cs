using System.ComponentModel.DataAnnotations;

namespace InsigniApi.Models.Dtos.ScoutGroup
{
    public class UpdateScoutGroupDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
