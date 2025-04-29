using InsigniApi.Data;
using InsigniApi.Models.Dtos.Assignment;
using InsigniApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsigniApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AssignmentController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        private GetAssignmentDto GetAssignmentDto(Assignment assignment) => new GetAssignmentDto
        {
            Id = assignment.Id,
            Name = assignment.Name,
            Description = assignment.Description
        };

        [HttpGet]
        public IActionResult GetAllAssignments()
        {
            var assignments = applicationDbContext.Assignments.Select(GetAssignmentDto);
            return Ok(assignments);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetAssignmentById(Guid id)
        {
            var assignment = applicationDbContext.Assignments.FirstOrDefault(a => a.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }
            var assignmentDto = GetAssignmentDto(assignment);
            return Ok(assignmentDto);
        }

        [HttpPost]
        public IActionResult AddAssignment([FromBody] AddAssignmentDto addAssignmentDto)
        {
            var Insignia = applicationDbContext.Insignias.Find(addAssignmentDto.InsigniaId);
            if (Insignia == null)
            {
                return NotFound("Insignia not found");
            }

            var existingAssignment = applicationDbContext.Assignments
                .FirstOrDefault(a => a.Name == addAssignmentDto.Name && a.InsigniaId == addAssignmentDto.InsigniaId);

            if (existingAssignment != null)
            {
                return BadRequest("Assignment already exists for this insignia.");
            }

            var assignment = new Assignment
            {
                Id = Guid.NewGuid(),
                Name = addAssignmentDto.Name,
                Description = addAssignmentDto.Description
            };
            applicationDbContext.Assignments.Add(assignment);
            applicationDbContext.SaveChanges();
            return CreatedAtAction(nameof(GetAssignmentById), new { id = assignment.Id }, GetAssignmentDto(assignment));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateAssignment(Guid id, [FromBody] UpdateAssignmentDto updateAssignmentDto)
        {
            var Insignia = applicationDbContext.Insignias.Find(updateAssignmentDto.InsigniaId);
            if (Insignia == null)
            {
                return NotFound("Insignia not found");
            }

            var existingAssignment = applicationDbContext.Assignments
                .FirstOrDefault(a => a.Name == updateAssignmentDto.Name && a.InsigniaId == updateAssignmentDto.InsigniaId);

            if (existingAssignment != null)
            {
                return BadRequest("Assignment already exists for this insignia.");
            }

            var assignment = applicationDbContext.Assignments.Find(id);
            if (assignment == null)
            {
                return NotFound();
            }
            assignment.Name = updateAssignmentDto.Name;
            assignment.Description = updateAssignmentDto.Description;
            applicationDbContext.SaveChanges();
            return NoContent();
        }
    }
}
