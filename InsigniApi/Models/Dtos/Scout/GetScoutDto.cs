namespace InsigniApi.Models.Dtos.Scout
{
    public class GetScoutDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Tenure { get; set; }
        public required string ScoutGroupId { get; set; }
        public required string ScoutGroupName { get; set; }
    }
}
