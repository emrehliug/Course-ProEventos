using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.DataContext;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {      
        private readonly IEventoService eventoService;
        public EventosController(IEventoService serviceEvento)
        {
            eventoService = serviceEvento;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Nenhum evento encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError
                 , $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await eventoService.GetAllEventoByIdAsync(id, true);
                if(evento == null) return NotFound("Nenhum evento encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError
                 , $"Erro ao tenta recupera o evento. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await eventoService.GetAllEventosBytemaAsync(tema, true);
                if(eventos == null) return NotFound("Nenhum evento encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError
                 , $"Erro ao tenta recupera eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await eventoService.AddEventos(model);
                if(evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                , $"Erro ao tenta adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await eventoService.UpdateEvento(id, model);
                if(evento == null) return BadRequest("Erro ao tentar atualizar evento por ID.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                , $"Erro ao tenta atualizar evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if(await eventoService.DeleteEvento(id))
                    return Ok("Deletado");
                else
                    return BadRequest("Evento não deletado");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                , $"Erro ao tenta deletar evento. Erro: {ex.Message}");
            }
        }
    }
}
