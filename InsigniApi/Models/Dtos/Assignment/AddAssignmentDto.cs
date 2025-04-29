namespace InsigniApi.Models.Dtos.Assignment
{
    public class AddAssignmentDto
    {
        public required String Name { get; set; }
        public required String Description { get; set; }
        public Guid InsigniaId { get; set; }
    }
}
