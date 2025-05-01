namespace InsigniApi.Models.Entities
{
    public class ScoutAssignment
    {
        public Guid ScoutId { get; set; }
        public Scout Scout { get; set; } = null!;
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;
        public DateTime DateCompleted { get; set; }
        public required string LeaderSignature { get; set; }
    }
}
