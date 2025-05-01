namespace InsigniApi.Models.Entities
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public required String Name { get; set; }
        public required String Description { get; set; }
        public Guid InsigniaId { get; set; }
        public Insignia? Insignia { get; set; }
        public List<ScoutAssignment> ScoutsWithAssignment { get; set; } = new List<ScoutAssignment>();
    }
}
