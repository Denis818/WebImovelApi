using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebImovelApi.Context;
using WebImovelApi.Entities;
using WebImovelApi.Services;

namespace WebImovelApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImoveisController : Controller
    {
        private readonly ImovelService _imovelService;

        public ImoveisController(ImovelService imovelService)
        {
            _imovelService = imovelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imovel>>> Get()
        {
            try
            {
                var imoveis = await _imovelService.FindAllAsync();

                if (imoveis is null)
                {
                    return NotFound("Imoveis não encontrados.");
                }

                return imoveis;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                             "Ocorreu um problema ao tratar sua solicitação.");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterImoveis")]
        public async Task<ActionResult<Imovel>> Get(int id)
        {
            var imoveis = await _imovelService.FindByIdAsync(id);

            if (imoveis == null)
            {
                return NotFound($"imovel com id= {id} não encontrado.");
            }

            return imoveis;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Imovel imovel)
        {
            if (imovel == null)
            {
                return BadRequest("Dados Inválidos.");
            }

            await _imovelService.InsertAsync(imovel);

            return new CreatedAtRouteResult("ObterImoveis", new { id = imovel.ImovelId }, imovel);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, Imovel imovel)
        {
            if (id != imovel.ImovelId)
            {
                return BadRequest("Dados inválidos.");
            }

            await _imovelService.UpdateAsync(imovel);

            return Ok(imovel);
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<Imovel>> Delete(int id)
        {
            var imovel = await _imovelService.FindByIdAsync(id);

            if (imovel == null)
            {
                return NotFound($"Imovel com id= {id} não encontrado.");
            }

            await _imovelService.RemoveAsync(id);
            return Ok(imovel);
        }
    }
}
