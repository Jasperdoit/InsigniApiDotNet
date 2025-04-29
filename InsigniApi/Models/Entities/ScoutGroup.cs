namespace InsigniApi.Models.Entities
{
    public class ScoutGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Scout> Scouts { get; set; } = new List<Scout>();
    }
}
