namespace InsigniApi.Models.Dtos.Scout
{
    public class GetScoutInsigniaDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int RequiredAssignments { get; set; }
        public List<GetScoutAssignmentDto> CompletedAssignments { get; set; } = new List<GetScoutAssignmentDto>();
        public List<GetScoutAssignmentDto> PendingAssignments { get; set; } = new List<GetScoutAssignmentDto>();
        public Boolean IsCompleted
        {
            get {
                if (CompletedAssignments.Count == 0)
                    return PendingAssignments.Count == 0;
                else
                {
                    return CompletedAssignments.Count >= RequiredAssignments;
                }
            }
        }
    }
}
