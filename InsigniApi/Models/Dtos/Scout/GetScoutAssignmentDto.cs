namespace InsigniApi.Models.Dtos.Scout
{
    public class GetScoutAssignmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCompleted { get; set; }
        public string LeaderSignature { get; set; } = string.Empty;
        public bool IsCompleted
        {
            get
            {
                return LeaderSignature != string.Empty;
            }
        }
    }
}
