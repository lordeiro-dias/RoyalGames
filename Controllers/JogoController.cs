using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly JogoService _service;

        public JogoController(JogoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerJogoDto>> Listar()
        {
            List<LerJogoDto> jogos = _service.Listar();

            return Ok(jogos);
        }

        [HttpGet("{id}")]
        public ActionResult<LerJogoDto> ObterPorId(int id)
        {
            try
            {
                LerJogoDto jogo = _service.ObterPorId(id);
                return Ok(jogo);
            }
            catch(DomainException ex)
            {
                return NotFound(ex.Message); 
            }
        }
    }
}
