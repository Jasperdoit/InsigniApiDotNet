using InsigniApi.Data;
using InsigniApi.Models.Dtos.Scout;
using InsigniApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsigniApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoutController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ScoutController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        private GetScoutDto GetScoutDto(Scout scout) => new GetScoutDto
        {
            Id = scout.Id,
            Name = scout.Name,
            Tenure = scout.Tennure,
            ScoutGroupId = scout.ScoutGroupId.ToString(),
            ScoutGroupName = scout.ScoutGroup != null ? scout.ScoutGroup.Name : "Unknown Group"
        };

        [HttpGet]
        public IActionResult GetAllScouts()
        {
            var scouts = applicationDbContext.Scouts
                .Include(s => s.ScoutGroup)
                .Select(GetScoutDto);

            return Ok(scouts);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetScoutById(Guid id)
        {
            var scout = applicationDbContext.Scouts
                .Include(s => s.ScoutGroup)
                .FirstOrDefault(s => s.Id == id);
            if (scout is null)
            {
                return NotFound();
            }
            return Ok(GetScoutDto(scout));
        }

        [HttpPost]
        public IActionResult AddScout(AddScoutDto addScoutDto)
        {

            // Check if the scout group exists
            var scoutGroup = applicationDbContext.ScoutGroups.Find(addScoutDto.ScoutGroupId);
            if (scoutGroup is null)
            {
                return BadRequest("Scout group does not exist.");
            }

            // Check if the scout already exists
            var existingScout = applicationDbContext.Scouts
                .FirstOrDefault(s => s.Name == addScoutDto.Name && s.ScoutGroupId == addScoutDto.ScoutGroupId);
            if (existingScout != null)
            {
                return BadRequest("Scout already exists in this scout group.");
            }

            var scout = new Scout
            {
                Id = Guid.NewGuid(),
                Name = addScoutDto.Name,
                Tennure = addScoutDto.Tennure,
                ScoutGroupId = addScoutDto.ScoutGroupId
            };
            applicationDbContext.Scouts.Add(scout);
            applicationDbContext.SaveChanges();
            return CreatedAtAction(nameof(GetScoutById), new { id = scout.Id }, GetScoutDto(scout));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateScout(Guid id, UpdateScoutDto updateScoutDto)
        {
            var scout = applicationDbContext.Scouts.Find(id);
            if (scout is null)
            {
                return NotFound();
            }
            // Check if the scout group exists
            var scoutGroup = applicationDbContext.ScoutGroups.Find(updateScoutDto.ScoutGroupId);
            if (scoutGroup is null)
            {
                return BadRequest("Scout group does not exist.");
            }
            scout.Name = updateScoutDto.Name;
            scout.Tennure = updateScoutDto.Tennure;
            scout.ScoutGroupId = updateScoutDto.ScoutGroupId;
            applicationDbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteScout(Guid id)
        {
            var scout = applicationDbContext.Scouts.Find(id);
            if (scout is null)
            {
                return NotFound();
            }
            applicationDbContext.Scouts.Remove(scout);
            applicationDbContext.SaveChanges();
            return NoContent();
        }

        [HttpPost("assignment")]
        public IActionResult AddAssignmentToScout(Guid scoutId, Guid assignmentId)
        {
            var scout = applicationDbContext.Scouts
                .Include(s => s.CompletedAssignments)
                .FirstOrDefault(s => s.Id == scoutId);
            if (scout is null)
            {
                return NotFound();
            }
            var assignment = applicationDbContext.Assignments.Find(assignmentId);
            if (assignment is null)
            {
                return NotFound();
            }
            if (scout.CompletedAssignments.Any(a => a.Id == assignmentId))
            {
                return BadRequest("Assignment already completed by this scout.");
            }
            scout.CompletedAssignments.Add(assignment);

            var insignia = applicationDbContext.Insignias
                .Include(i => i.Assignments)
                .FirstOrDefault(i => i.Assignments.Any(a => a.Id == assignmentId));
            if (insignia != null)
            {
                // Check the pre-requisite assignments for the insignia
                var requiredAssignmentsCount = insignia.RequiredAssignments;

                //If all assignments need to be completed.
                if (requiredAssignmentsCount == 0)
                {
                    List<Assignment> requiredAssignments = insignia.Assignments;
                    var allAssignmentsCompleted = requiredAssignments.All(a => scout.CompletedAssignments.Any(ca => ca.Id == a.Id));
                    if (allAssignmentsCompleted)
                    {
                        scout.CompletedInsignias.Add(insignia);
                    }
                }
                else
                {
                    //Grant insignia if the required number of assignments are completed.
                    var completedAssignmentsCount = scout.CompletedAssignments.Count(a => insignia.Assignments.Any(ia => ia.Id == a.Id));
                    if (completedAssignmentsCount >= requiredAssignmentsCount)
                    {
                        scout.CompletedInsignias.Add(insignia);
                    }
                }
            }
                applicationDbContext.SaveChanges();
                return NoContent();
        }
    }
}
