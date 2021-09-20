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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepository _repository;
        private readonly ITagRepository _tagRepository;

        public EmpresaController(IEmpresaRepository repository, ITagRepository tagRepository)
        {
            this._repository = repository;
            this._tagRepository = tagRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmpresaDTO empresaDto)
        {
            Empresa empresa = new Empresa
            {
                nombre = empresaDto.nombre,
                ruc = empresaDto.ruc,
                fecha_registro = DateTime.Now
            };
            var insertedId = await _repository.CreateAsync(empresa);
            foreach (var tag in empresaDto.tags)
            {
                if (tag.selected) {
                    EmpresaTag empresaTag = new EmpresaTag { 
                        empresaId = insertedId,
                        tagId = tag.tagId
                    };
                    await _tagRepository.CreateAsync(empresaTag);
                }
            }
            return Ok(insertedId);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Empresa>> FindById(int id) {
            try
            {
                var empresa = await _repository.FindByIdAsync(id);
                return Ok(empresa);
            }
            catch (Exception)
            {
                return NotFound($"Empresa con id {id} no existe");
            }
        }
        [HttpGet]
        public async Task<IEnumerable<Empresa>> ToList()
        {
            return await _repository.ToListAsync();
        }
    }
}
