using InsigniApi.Models.Dtos.Assignment;

namespace InsigniApi.Models.Dtos.Insignia
{
    public class GetInsigniaDto
    {
        public Guid Id { get; set; }
        public required String Name { get; set; }
        public required String Description { get; set; }
        public required String ImageUrl { get; set; }
        public List<GetAssignmentDto> Assignments { get; set; } = new List<GetAssignmentDto>();
    }
}
