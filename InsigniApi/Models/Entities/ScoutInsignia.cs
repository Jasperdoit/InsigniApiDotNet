namespace InsigniApi.Models.Entities
{
    public class ScoutInsignia
    {
        public Guid ScoutId { get; set; }
        public Scout Scout { get; set; } = null!;
        public Guid InsigniaId { get; set; }
        public Insignia Insignia { get; set; } = null!;
        public DateTime DateAwarded { get; set; }
    }
}
