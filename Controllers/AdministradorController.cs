using Microsoft.AspNetCore.Mvc;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Servicio;
namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly ServAdministrador _servAdministrador;
        public AdministradorController(ServAdministrador servAdministrador)
        {
            _servAdministrador = servAdministrador;
        }
        [HttpPost("crear")]
        public async Task<IActionResult> CrearAdministrador([FromBody] DtoRegistroAdministrador dto)
        {
            try
            {
                var resultado = await _servAdministrador.CrearAdministrador(dto);
                return Ok(new { mensaje = "Administrador creado exitosamente", data = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAdministrador([FromBody] DtoLoginAdministrador dto)
        {
            try
            {
                var resultado = await _servAdministrador.LoginAdministrador(dto.Email, dto.Password);
                if (resultado == null)
                {
                    return Unauthorized(new { mensaje = "Credenciales inválidas" });
                }
                return Ok(new { mensaje = "Login exitoso", data = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpGet("listar")]
        public async Task<IActionResult> ListarAdministradores()
        {
            try
            {
                var resultado = await _servAdministrador.ListarAdministradores();
                return Ok(new { mensaje = "Administradores obtenidos exitosamente", data = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpGet("perfil/{id}")]
        public async Task<IActionResult> ObtenerPerfil(int id)
        {
            try
            {
                var resultado = await _servAdministrador.ObtenerPerfilAdministrador(id);
                if (resultado == null)
                {
                    return NotFound(new { mensaje = "Administrador no encontrado" });
                }
                return Ok(new { mensaje = "Perfil obtenido exitosamente", data = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPut("actualizar")]
        public async Task<IActionResult> ActualizarPerfil([FromBody] DtoPerfilAdministrador dto)
        {
            try
            {
                var resultado = await _servAdministrador.ActualizarPerfilAdministrador(dto);
                return Ok(new { mensaje = "Perfil actualizado exitosamente", data = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPut("cambiar-estado/{id}")]
        public async Task<IActionResult> CambiarEstado(int id)
        {
            try
            {
                var resultado = await _servAdministrador.CambiarEstadoAdministrador(id);
                if (!resultado)
                {
                    return NotFound(new { mensaje = "Administrador no encontrado" });
                }
                return Ok(new { mensaje = "Estado cambiado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpGet("estadisticas")]
        public async Task<IActionResult> ObtenerEstadisticas()
        {
            try
            {
                var resultado = await _servAdministrador.ObtenerEstadisticas();
                return Ok(new { mensaje = "Estadísticas obtenidas exitosamente", data = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
