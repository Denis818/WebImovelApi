using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebImovelApi.Context;
using WebImovelApi.Entities;
using WebImovelApi.Services;

namespace WebImovelApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocadoresController : Controller
    {
        private readonly LocadorService _locadorService;

        public LocadoresController(LocadorService locadorService)
        {
            _locadorService = locadorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locador>>> Get()
        {
            try
            {
                var locador = await _locadorService.FindAllAsync();

                if (locador is null)
                {
                    return NotFound("Locadores não encontrados.");
                }

                return locador;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                             "Ocorreu um problema ao tratar sua solicitação.");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterLocador")]
        public async Task<ActionResult<Locador>> Get(int id)
        {
            var locadores = await _locadorService.FindByIdAsync(id);

            if (locadores == null)
            {
                return NotFound($"Locador com id= {id} não encontrado.");
            }
            return Ok(locadores);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Locador locador)
        {
            if (locador is null)
            {
                return BadRequest("Dados inválidos.");
            }

            await _locadorService.InsertAsync(locador);

            return new CreatedAtRouteResult("ObterLocador",
                new { id = locador.LocadorId }, locador);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, Locador locador)
        {
            if (id != locador.LocadorId)
            {
                return BadRequest("Dados inválidos.");
            }

            await _locadorService.UpdateAsync(locador);

            return Ok(locador);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            var locador = await _locadorService.FindByIdAsync(id);

            if (locador == null)
            {
                return NotFound($"Locador com id= {id} não encontrado.");
            }

            await _locadorService.RemoveAsync(id);
            return Ok(locador);
        }
    }
}
