namespace InsigniApi.Models.Dtos.Assignment
{
    public class GetAssignmentDto
    {
        public Guid Id { get; set; }
        public required String Name { get; set; }
        public required String Description { get; set; }
    }
}
