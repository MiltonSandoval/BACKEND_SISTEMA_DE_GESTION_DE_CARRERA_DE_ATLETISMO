using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Services;
using Microsoft.AspNetCore.Mvc;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Mapper;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;


namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetidorController : ControllerBase
    {
        private readonly ServCompetidor _servCompetidor;

        public CompetidorController(ServCompetidor servCompetidor)
        {
            _servCompetidor = servCompetidor;
        }

        [HttpGet("ListarTodos")]
        public ActionResult<List<DtoPerfilUsuario>> Get()
        {
            try
            {
                var Competidores = _servCompetidor.ObtenerCompetidoresActivos();
                return Ok(Competidores);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("Registrar")]
        public async Task<ActionResult<DtoPerfilUsuario>> Post([FromBody] DtoRegistroUsuario dtoRegistroUsuario)
        {
            try
            {
                DtoPerfilUsuario competidor = await _servCompetidor.RegistrarCompetidor(dtoRegistroUsuario);
                if (competidor != null)
                {
                    return Ok(competidor);
                }
                else
                {
                    return BadRequest("message:{Error al registrar el competidor.}");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Buscar/{id_user}/{id_persona}/")]
        public IActionResult Get(int id_persona, int Id_user)
        {
            // Aquí podrías buscar el competidor por ID en una base de datos o lista
            return Ok($"Detalles del competidor con ID: {id_persona}");
        }
        //[HttpPost("Registrar")]
        //public IActionResult Post([FromBody] string competidor)
        //{
        //    // Aquí podrías agregar el competidor a una base de datos o lista
        //    //return CreatedAtAction(nameof(Get), new { id = 1 }, competidor);
        //}

    }
}
