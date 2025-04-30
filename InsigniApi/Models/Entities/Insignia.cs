namespace InsigniApi.Models.Entities
{
    public class Insignia
    {
        public Guid Id { get; set; }
        public required String Name { get; set; }
        public required String Description { get; set; }
        public required String ImageUrl { get; set; }
        public int RequiredAssignments { get; set; }
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<Scout> ScoutsWithInsignia { get; set; } = new List<Scout>();
    }
}
