namespace InsigniApi.Models.Entities
{
    public class Scout
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Tennure { get; set; }
        public Guid ScoutGroupId { get; set; }
        public ScoutGroup? ScoutGroup { get; set; }
        public List<ScoutAssignment> CompletedAssignments { get; set; } = new List<ScoutAssignment>();
        public List<ScoutInsignia> CompletedInsignias { get; set; } = new List<ScoutInsignia>();
    }
}
