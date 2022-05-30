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
    public class ClientesController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {      
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            try
            {
                var clientes = await _clienteService.FindAllAsync();

                if (clientes is null)
                {
                    return NotFound("Clientes não encontrados");
                }

                return clientes;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                             "Ocorreu um problema ao tratar sua solicitação.");
            }      
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterClientes")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _clienteService.FindByIdAsync(id);

            if (cliente is null)
            {
                return NotFound($"Cliente com id= {id} não encontrado.");
            }

            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Cliente cliente)
        {
            if (cliente is null)
            {
                return NotFound("Dados inválidos.");
            }

            await _clienteService.InsertAsync(cliente);

            return new CreatedAtRouteResult("ObterClientes", new { id = cliente.ClienteId }, cliente);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, Cliente cliente)
        {

            if (id != cliente.ClienteId)
            {
                return NotFound($"Id= {cliente.ClienteId} não encontrado");
            }

            await _clienteService.UpdateAsync(cliente);

            return Ok(cliente);
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<Cliente>> Delete(int id)
        {
            var cliente = await _clienteService.FindByIdAsync(id);

            if (cliente == null)
            {
                return NotFound($"Cliente com id= {id} não encontrado.");
            }

            await _clienteService.RemoveAsync(id);
            return Ok(cliente);
        }
    }
}
