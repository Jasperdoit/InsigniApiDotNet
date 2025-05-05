using System.ComponentModel.DataAnnotations;

namespace InsigniApi.Models.Dtos.Scout
{
    public class RemoveAssignmentFromScoutDto
    {
        [Required]
        public Guid ScoutId { get; set; }
        [Required]
        public Guid AssignmentId { get; set; }
    }
}
