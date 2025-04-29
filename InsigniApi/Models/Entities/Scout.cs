namespace InsigniApi.Models.Entities
{
    public class Scout
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Tennure { get; set; }
        public Guid ScoutGroupId { get; set; }
        public ScoutGroup? ScoutGroup { get; set; }
        // Todo: Add navigational properties for Completed Assignments and Insignias. How should Insignias be assigned in response to completed assignments?
    }
}
