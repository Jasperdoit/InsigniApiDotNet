using System.ComponentModel.DataAnnotations;

namespace InsigniApi.Models.Dtos.Assignment
{
    public class AddAssignmentDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required String Name { get; set; }
        [Required]
        [StringLength(2048, MinimumLength = 3)]
        public required String Description { get; set; }
        [Required]
        public Guid InsigniaId { get; set; }
    }
}
