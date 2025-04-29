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
    }
}
