using InsigniApi.Data;
using InsigniApi.Models.Dtos.Insignia;
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

        private GetScoutDto GetScoutDto(Scout scout)
        {
            // TODO: Add Description to AssignmentDto (It is missing)
            // TODO: Rewrite this to be done without linq, this is too hard to read and to manage and it doens't work right now :(
            // TODO: Make it so that if there is no progress on an assignment it doesn't show up in the list.

            List<GetScoutInsigniaDto> completedInsignias = new();

            List<Insignia> insignias =
                applicationDbContext.ScoutInsignias
                    .Include(si => si.Insignia)
                    .Where(si => si.ScoutId == scout.Id)
                    .Select(si => si.Insignia)
                    .ToList();
            // Populate completed insignias
            foreach (Insignia insignia in insignias)
            {
                List<Assignment> assignmentsScoutHasCompletedForInsignia = applicationDbContext.ScoutAssignments
                    .Include(sa => sa.Assignment)
                    .Where(sa => sa.ScoutId == scout.Id && sa.Assignment.InsigniaId == insignia.Id)
                    .Select(sa => sa.Assignment)
                    .ToList();

                List<Assignment> assignmentsOfInsignia = applicationDbContext.Assignments
                    .Where(a => a.InsigniaId == insignia.Id)
                    .ToList();

                bool hasCompletedInsignia = false;

                // If All Assignments are required.
                if (insignia.RequiredAssignments == 0)
                {
                    hasCompletedInsignia = assignmentsOfInsignia.Count == assignmentsScoutHasCompletedForInsignia.Count;
                }
                // Else, only some assignments are required, the number in question mentioned.
                else
                {
                    hasCompletedInsignia = assignmentsScoutHasCompletedForInsignia.Count >= assignmentsOfInsignia.Count;
                }

                List<GetScoutAssignmentDto> completedAssignments = applicationDbContext.ScoutAssignments
                    .Include(sa => sa.Assignment)
                    .Where(sa => sa.ScoutId == scout.Id && sa.Assignment.InsigniaId == insignia.Id)
                    .Select(sa => new GetScoutAssignmentDto
                    {
                        Id = sa.AssignmentId,
                        DateCompleted = sa.DateCompleted,
                        Name = sa.Assignment.Name,
                        Description = sa.Assignment.Description,
                        LeaderSignature = sa.LeaderSignature

                    })
                    .ToList();

                List<GetScoutAssignmentDto> pendingAssignments = assignmentsOfInsignia
                    .Where(a => !completedAssignments.Any(ca => ca.Id == a.Id))
                    .Select(a => new GetScoutAssignmentDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Description = a.Description,
                        DateCompleted = default(DateTime),
                        LeaderSignature = String.Empty
                        
                    })
                    .ToList();
                
                if(hasCompletedInsignia)
                    completedInsignias.Add(new GetScoutInsigniaDto
                    {
                        Id = insignia.Id,
                        ImageUrl = insignia.ImageUrl,
                        CompletedAssignments = completedAssignments,
                        // PendingAssignment = inside insignia.Assignments but NOT in CompletedAssignments
                        PendingAssignments = pendingAssignments
                    });
            }

            // Populate in progress insignias
            // This is an insignia that has atleast one assignment completed, but not the required amount of assignments.

            List<GetScoutInsigniaDto> inProgressInsignias = new();

            List<Insignia> insigniasInProgress =
                applicationDbContext.Insignias
                    .Include(i => i.Assignments)
                    .Where(i => !applicationDbContext.ScoutInsignias
                        .Any(si => si.ScoutId == scout.Id && si.InsigniaId == i.Id))
                    .ToList();

            foreach (Insignia insignia in insigniasInProgress)
            {
                List<ScoutAssignment> scoutAssignments = applicationDbContext.ScoutAssignments
                    .Include(sa => sa.Assignment)
                    .Where(sa => sa.ScoutId == scout.Id && sa.Assignment.InsigniaId == insignia.Id)
                    .ToList();

                if (scoutAssignments.Count == 0)
                    continue;

                List<GetScoutAssignmentDto> completedAssignmentDtos = scoutAssignments.Select(sa => new GetScoutAssignmentDto
                {
                    Name = sa.Assignment.Name,
                    Description = sa.Assignment.Description,
                    Id = sa.AssignmentId,
                    DateCompleted = sa.DateCompleted,
                    LeaderSignature = sa.LeaderSignature
                }).ToList();

                List<GetScoutAssignmentDto> pendingAssignmentDtos = insignia.Assignments
                    .Where(a => !completedAssignmentDtos.Any(ca => ca.Id == a.Id))
                    .Select(a => new GetScoutAssignmentDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Description = a.Description,
                        DateCompleted = default(DateTime),
                        LeaderSignature = String.Empty
                    })
                    .ToList();

                inProgressInsignias.Add(new GetScoutInsigniaDto
                {
                    Id = insignia.Id,
                    Name = insignia.Name,
                    ImageUrl = insignia.ImageUrl,
                    RequiredAssignments = insignia.RequiredAssignments,
                    CompletedAssignments = completedAssignmentDtos,
                    PendingAssignments = pendingAssignmentDtos
                });
            }

            return new GetScoutDto
            {
                Id = scout.Id,
                Name = scout.Name,
                Tenure = scout.Tennure,
                ScoutGroupId = scout.ScoutGroupId.ToString(),
                ScoutGroupName = scout.ScoutGroup != null ? scout.ScoutGroup.Name : "Unknown Group",
                CompletedInsignias = completedInsignias,
                InProgressInsignias = inProgressInsignias
            };
        }

        [HttpGet]
        public IActionResult GetAllScouts()
        {
            var scouts = applicationDbContext.Scouts
                .Include(s => s.ScoutGroup)
                .ToList();

            return Ok(scouts.Select(GetScoutDto));
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
        public IActionResult AddScoutAssignment(AddAssignmentToScoutDto addScoutAssignmentDto)
        {
            var scout = applicationDbContext.Scouts.Find(addScoutAssignmentDto.ScoutId);
            if (scout is null)
            {
                return NotFound("Scout not found.");
            }
            var assignment = applicationDbContext.Assignments
                .Find(addScoutAssignmentDto.AssignmentId);
            if (assignment is null)
            {
                return NotFound("Assignment not found.");
            }
            var insignia = applicationDbContext.Insignias
                .Find(assignment.InsigniaId);

            var scoutAssignment = new ScoutAssignment
            {
                ScoutId = addScoutAssignmentDto.ScoutId,
                AssignmentId = addScoutAssignmentDto.AssignmentId,
                LeaderSignature = addScoutAssignmentDto.LeaderSignature,
                DateCompleted = DateTime.UtcNow
            };
            applicationDbContext.ScoutAssignments.Add(scoutAssignment);

            if (insignia != null)
            {
                // If Required assignments is 0, then all assignments are needed to be completed
                if (insignia.RequiredAssignments == 0)
                {
                    var hasCompletedAllAssignments = applicationDbContext.ScoutAssignments
                        .Count(sa => sa.ScoutId == scout.Id && sa.Assignment.InsigniaId == insignia.Id) >=
                        applicationDbContext.Assignments.Count(a => a.InsigniaId == insignia.Id);

                    if (hasCompletedAllAssignments)
                    {
                        var scoutInsignia = new ScoutInsignia
                        {
                            ScoutId = scout.Id,
                            InsigniaId = insignia.Id
                        };
                        applicationDbContext.ScoutInsignias.Add(scoutInsignia);
                    }
                }
                else
                {
                    // If Required assignments is not 0, then only the required number of assignments are needed to be completed
                    var completedAssignments = applicationDbContext.ScoutAssignments
                        .Count(sa => sa.ScoutId == scout.Id && sa.Assignment.InsigniaId == insignia.Id);
                    if (completedAssignments >= insignia.RequiredAssignments)
                    {
                        var scoutInsignia = new ScoutInsignia
                        {
                            ScoutId = scout.Id,
                            InsigniaId = insignia.Id
                        };
                        applicationDbContext.ScoutInsignias.Add(scoutInsignia);
                    }
                }
            }
            applicationDbContext.SaveChanges();
            return CreatedAtAction(nameof(GetScoutById), new { id = scout.Id }, GetScoutDto(scout));
        }
    }
}
