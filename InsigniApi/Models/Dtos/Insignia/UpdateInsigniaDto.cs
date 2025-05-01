namespace InsigniApi.Models.Dtos.Insignia
{
    public class UpdateInsigniaDto
    {
        public required String Name { get; set; }
        public required String ImageUrl { get; set; }
        public int RequiredAssignments { get; set; }
    }
}
