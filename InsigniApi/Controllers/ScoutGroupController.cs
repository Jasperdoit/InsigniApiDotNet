using InsigniApi.Data;
using InsigniApi.Models.Dtos.ScoutGroup;
using InsigniApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsigniApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoutGroupController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ScoutGroupController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private GetScoutGroupDto GetScoutGroupDto(ScoutGroup scoutGroup) => new GetScoutGroupDto
        {
            Id = scoutGroup.Id,
            Name = scoutGroup.Name
        };

        [HttpGet]
        public IActionResult GetAllScoutGroups()
        {
            return Ok(dbContext.ScoutGroups.Select(GetScoutGroupDto));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetScoutGroupById(Guid id)
        {
            var scoutGroup = dbContext.ScoutGroups.Find(id);
            if (scoutGroup is null)
            {
                return NotFound();
            }
            return Ok(GetScoutGroupDto(scoutGroup));
        }

        [HttpPost]
        public IActionResult AddScoutGroup(AddScoutGroupDto addScoutGroupDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scoutGroup = new ScoutGroup
            {
                Id = Guid.NewGuid(),
                Name = addScoutGroupDto.Name
            };
            dbContext.ScoutGroups.Add(scoutGroup);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetScoutGroupById), new { id = scoutGroup.Id }, GetScoutGroupDto(scoutGroup));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateScoutGroup(Guid id, UpdateScoutGroupDto updateScoutGroupDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scoutGroup = dbContext.ScoutGroups.Find(id);
            if (scoutGroup is null)
            {
                return NotFound();
            }
            scoutGroup.Name = updateScoutGroupDto.Name;
            dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteScoutGroup(Guid id)
        {
            var scoutGroup = dbContext.ScoutGroups.Find(id);
            if (scoutGroup is null)
            {
                return NotFound();
            }
            dbContext.ScoutGroups.Remove(scoutGroup);
            dbContext.SaveChanges();
            return NoContent();
        }

    }
}
