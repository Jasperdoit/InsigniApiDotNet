using System.ComponentModel.DataAnnotations;

namespace InsigniApi.Models.Dtos.Scout
{
    public class AddAssignmentToScoutDto
    {
        [Required]
        public Guid ScoutId { get; set; }
        [Required]
        public Guid AssignmentId { get; set; }
        [Required]
        [StringLength(100)]
        public required String LeaderSignature { get; set; }
    }
}
