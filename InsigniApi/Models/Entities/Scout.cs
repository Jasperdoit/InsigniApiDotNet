namespace InsigniApi.Models.Entities
{
    public class Scout
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Tennure { get; set; }
        public Guid ScoutGroupId { get; set; }
        public ScoutGroup? ScoutGroup { get; set; }
        public List<Assignment> CompletedAssignments { get; set; } = new List<Assignment>();
        public List<Insignia> CompletedInsignias { get; set; } = new List<Insignia>();
    }
}
