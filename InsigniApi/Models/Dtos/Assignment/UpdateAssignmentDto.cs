namespace InsigniApi.Models.Dtos.Assignment
{
    public class UpdateAssignmentDto
    {
        public required String Name { get; set; }
        public required String Description { get; set; }
        public Guid InsigniaId { get; set; }
    }
}
