using Microsoft.AspNetCore.Mvc;
using PruebaWebAPI.Models;
using PruebaWebAPI.Models.DTOs;
using PruebaWebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository repository;

        public TagController(ITagRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<Tag>> Create([FromBody] CreateTagDTO createTagDto) {
            try
            {
                Tag tag = new Tag
                {
                    nombre = createTagDto.nombre
                };
                int insertedId = await repository.CreateTagAsync(tag);
                var createdTag = await repository.FindByIdAsync(insertedId);
                return Ok(createdTag);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IEnumerable<Tag>> ToList()
        {
            return await repository.ToListAsync();
        }
    }
}
