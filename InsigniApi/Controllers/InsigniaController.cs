﻿using InsigniApi.Data;
using InsigniApi.Models.Dtos.Assignment;
using InsigniApi.Models.Dtos.Insignia;
using InsigniApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsigniApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsigniaController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public InsigniaController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        private GetInsigniaDto GetInsigniaDto(Insignia insignia) => new GetInsigniaDto
        {
            Id = insignia.Id,
            Name = insignia.Name,
            ImageUrl = insignia.ImageUrl,
            Assignments = insignia.Assignments.Select(a => new GetAssignmentDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            }).ToList()
        };


        [HttpGet]
        public IActionResult GetAllInsignias()
        {
            var insignias = applicationDbContext.Insignias
                .Include(i => i.Assignments)
                .Select(GetInsigniaDto);
            return Ok(insignias);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetInsigniaById(Guid id)
        {
            var insignia = applicationDbContext.Insignias
                .Include(i => i.Assignments)
                .FirstOrDefault(i => i.Id == id);
            if (insignia == null)
            {
                return NotFound();
            }
            var insigniaDto = GetInsigniaDto(insignia);
            return Ok(insigniaDto);
        }

        [HttpPost]
        public IActionResult AddInsignia(AddInsigniaDto addInsigniaDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var insignia = new Insignia
            {
                Name = addInsigniaDto.Name,
                ImageUrl = addInsigniaDto.ImageUrl,
                RequiredAssignments = 0,
            };
            applicationDbContext.Insignias.Add(insignia);
            applicationDbContext.SaveChanges();
            return CreatedAtAction(nameof(GetInsigniaById), new { id = insignia.Id }, GetInsigniaDto(insignia));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateInsignia(Guid id, UpdateInsigniaDto updateInsigniaDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var insignia = applicationDbContext.Insignias.Find(id);
            if (insignia == null)
            {
                return NotFound();
            }
            insignia.Name = updateInsigniaDto.Name;
            insignia.ImageUrl = updateInsigniaDto.ImageUrl;
            insignia.RequiredAssignments = updateInsigniaDto.RequiredAssignments;
            applicationDbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteInsignia(Guid id)
        {
            var insignia = applicationDbContext.Insignias.Find(id);
            if (insignia == null)
            {
                return NotFound();
            }
            applicationDbContext.Insignias.Remove(insignia);
            applicationDbContext.SaveChanges();
            return NoContent();
        }
    }
}
