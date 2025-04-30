namespace InsigniApi.Models.Dtos.Scout
{
    public class AddAssignmentToScoutDto
    {
        public Guid ScoutId { get; set; }
        public Guid AssignmentId { get; set; }
        public required String LeaderSignature { get; set; }
    }
}
