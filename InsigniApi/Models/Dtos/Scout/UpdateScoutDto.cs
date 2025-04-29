namespace InsigniApi.Models.Dtos.Scout
{
    public class UpdateScoutDto
    {
        public required string Name { get; set; }
        public int Tennure { get; set; }
        public Guid ScoutGroupId { get; set; }
    }
}
