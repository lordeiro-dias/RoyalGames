using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.UsuarioDto;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        // listar todos os usuarios
        [HttpGet]
        public ActionResult<List<LerUsuarioDto>> Listar()
        {
            List<LerUsuarioDto> usuarios = _service.Listar();

            return Ok(usuarios);
        }

        // listar usuario por id
        [HttpGet("{id}")]
        public ActionResult<LerUsuarioDto> ObterPorId(int id)
        {
            try
            {
                LerUsuarioDto usuario = _service.ObterPorId(id);
                return Ok(usuario);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // listar usuario por email
        [HttpGet("email/{email}")]
        public ActionResult<LerUsuarioDto> ObterPorEmail(string email)
        {
            try
            {
                LerUsuarioDto usuario = _service.ObterPorEmail(email);
                return Ok(usuario);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // adicionar usuario
        [HttpPost]
        public ActionResult<LerUsuarioDto> Adicionar(CriarUsuarioDto usuarioDto)
        {
            try
            {
                LerUsuarioDto usuarioCriado = _service.Adicionar(usuarioDto);
                return StatusCode(201, usuarioCriado);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // atualizar usuario por id
        [HttpPut("{id}")]
        public ActionResult<LerUsuarioDto> Atualizar(int id, CriarUsuarioDto usuarioDto)
        {
            try
            {
                LerUsuarioDto usuarioAtualizado = _service.Atualizar(id, usuarioDto);
                return Ok(usuarioAtualizado);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        // deletar(inativar) usuario por id
        [HttpDelete("{id}")]
        public ActionResult<LerUsuarioDto> Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
