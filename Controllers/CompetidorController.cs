using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Services;
using Microsoft.AspNetCore.Mvc;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using MySql.Data.MySqlClient;

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
                if (!(competidor is null))
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

        [HttpGet("Login")]
        public async Task<ActionResult<DtoPerfilUsuario>> Get(string Email, string Password)
        {
            try
            {

                DtoPerfilUsuario competidor = await _servCompetidor.LoginCompetidor(Email, Password);
                return Ok(competidor);
            }
            catch
            {
                return NotFound(new
                {
                    mensaje = "Credenciales incorrectas o competidor no encontrado."
                });
            }
        }

        [HttpPatch("ActualizarEstadoCompetidor/{id}")]
        public async Task<ActionResult<DtoPerfilUsuario>> ActualizarEstadoCompetidor(int id)
        {
            try
            {
                DtoPerfilUsuario NuevoEstado = await _servCompetidor.ActualizarEstadoCompetidor(id);
                return Ok(NuevoEstado);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("ActualizarPerfilCompetidor/")]
        public async Task<ActionResult<DtoPerfilUsuario>> ActualizarPerfilCompetidor([FromBody] DtoPerfilUsuario dtoPerfilUsuario)
        {
            try
            {
                DtoPerfilUsuario competidor = await _servCompetidor.ActualizarPerfilCompetidor(dtoPerfilUsuario);
                return Ok(competidor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("SolicitarUnirseClub/")]
        public async Task<ActionResult<string>> PostSolicitarUnirse(int id_club, int id_competidor)
        {
            try
            {
                bool Solicitud = await _servCompetidor.SolicitarUnirseClub(id_club, id_competidor);
                if (Solicitud)
                    return Ok(new { mensaje = "Solicitud enviada" });
                return NotFound(new { mensaje = "Error al solicitar unirse al club, asegurate de no inscrito en uno." });
            }
            catch (MySqlException ex)
            {
                return BadRequest($"Error en la base de datos:{ex.Message}");
            }
            catch (Exception ex2)
            {
                return ("Error interno, verificar que los datos ingresados sean los correctos");
            }



        }
    }
}

