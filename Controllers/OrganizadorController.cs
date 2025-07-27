using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Services;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizadorController : ControllerBase
    {
        private readonly ServOrganizador _servOrganizador;

        public OrganizadorController(ServOrganizador servOrganizador)
        {
            _servOrganizador = servOrganizador;
        }

        [HttpPost("RegistrarOrganizador")]
        public async Task<ActionResult<DtoPerfilOrganizador>> Post([FromBody] DtoRegistroUsuario dtoRegistroUsuario)
        {
            try
            {
                DtoPerfilOrganizador organizador = await _servOrganizador.RegistrarOrganizador(dtoRegistroUsuario);
                if (!(organizador is null))
                {
                    return Ok(organizador);
                }
                else
                {
                    return BadRequest(new
                    {
                        mensaje = "Error al registrar el organizador."
                    });
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Login")]
        public async Task<ActionResult<DtoPerfilOrganizador>> Get(string Email, string Password)
        {
            try
            {
                DtoPerfilOrganizador organizador = await _servOrganizador.LoginOrganizador(Email, Password);
                return Ok(organizador);
            }
            catch
            {
                return NotFound(new
                {
                    mensaje = "Credenciales incorrectas o organizador no encontrado."
                });
            }
        }

        [HttpPut("ActualizarPerfilOrganizador")]
        public async Task<ActionResult<DtoPerfilOrganizador>> ActualizarPerfilOrganizador([FromBody] DtoPerfilOrganizador dtoPerfilOrganizador)
        {
            try
            {
                DtoPerfilOrganizador organizador = await _servOrganizador.ActualizarPerfilOrganizador(dtoPerfilOrganizador);
                return Ok(organizador);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        // AGREGAR ESTE NUEVO MÉTODO al controlador existente

        [HttpPut("ActualizarDatos")]
        public async Task<ActionResult<DtoPerfilOrganizador>> ActualizarDatos([FromBody] DtoActualizarOrganizador datos)
        {
            try
            {
                DtoPerfilOrganizador organizador = await _servOrganizador.ActualizarDatosOrganizador(datos);
                if (organizador != null)
                {
                    return Ok(organizador);
                }
                else
                {
                    return BadRequest(new
                    {
                        mensaje = "Error al actualizar los datos del organizador."
                    });
                }
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        // TAMBIÉN AGREGAR UN ENDPOINT PARA OBTENER PERFIL POR ID
        [HttpGet("ObtenerPerfil/{idOrganizador}")]
        public async Task<ActionResult<DtoPerfilOrganizador>> ObtenerPerfil(int idOrganizador)
        {
            try
            {
                DtoPerfilOrganizador organizador = await _servOrganizador.ObtenerPerfilOrganizador(idOrganizador);
                if (organizador != null)
                {
                    return Ok(organizador);
                }
                else
                {
                    return NotFound(new { mensaje = "Organizador no encontrado." });
                }
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        // NUEVO ENDPOINT PARA CAMBIAR CONTRASEÑA
        [HttpPut("CambiarPassword")]
        public async Task<ActionResult<bool>> CambiarPassword([FromBody] DtoCambiarPassword datos)
        {
            try
            {
                bool exito = await _servOrganizador.CambiarPasswordOrganizador(datos);
                if (exito)
                {
                    return Ok(new { mensaje = "Contraseña actualizada exitosamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al cambiar la contraseña." });
                }
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }
        [HttpPost("CrearCarrera")]
        public async Task<ActionResult<DtoCarrera>> CrearCarrera([FromBody] DtoCrearCarrera dtoCrearCarrera)
        {
            try
            {
                DtoCarrera carrera = await _servOrganizador.CrearCarrera(dtoCrearCarrera);
                if (!(carrera is null))
                {
                    return Ok(carrera);
                }
                else
                {
                    return BadRequest(new
                    {
                        mensaje = "Error al crear la carrera."
                    });
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("EditarCarrera")]
        public async Task<ActionResult<DtoCarrera>> EditarCarrera([FromBody] DtoCarrera dtoCarrera)
        {
            try
            {
                DtoCarrera carrera = await _servOrganizador.EditarCarrera(dtoCarrera);
                return Ok(carrera);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("CarrerasDelOrganizador/{idOrganizador}")]
        public async Task<ActionResult<List<DtoCarrera>>> GetCarrerasDelOrganizador(int idOrganizador)
        {
            try
            {
                List<DtoCarrera> carreras = await _servOrganizador.ObtenerCarrerasDelOrganizador(idOrganizador);
                if (carreras.Count > 0)
                {
                    return Ok(carreras);
                }
                return BadRequest(new
                {
                    mensaje = "No se encontraron carreras para este organizador."
                });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("AñadirResultados")]
        public async Task<ActionResult<bool>> AñadirResultados([FromBody] List<DtoResultadoCarrera> resultados)
        {
            try
            {
                // VALIDAR QUE LA LISTA NO ESTÉ VACÍA
                if (resultados == null || resultados.Count == 0)
                {
                    return BadRequest(new { mensaje = "Debe proporcionar al menos un resultado." });
                }
                bool exito = await _servOrganizador.AñadirResultadosCarrera(resultados);
                if (exito)
                {
                    return Ok(new { mensaje = "Resultados añadidos exitosamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al añadir los resultados." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPatch("CambiarEstadoCarrera/{idCarrera}")]
        public async Task<ActionResult<bool>> CambiarEstadoCarrera(int idCarrera)
        {
            try
            {
                bool exito = await _servOrganizador.CambiarEstadoCarrera(idCarrera);
                if (exito)
                {
                    return Ok(new { mensaje = "Estado de carrera actualizado." });
                }
                else
                {
                    return BadRequest(new { mensaje = "Error al cambiar el estado." });
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ListarCategorias")]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            try
            {
                List<Categoria> categorias = await _servOrganizador.ListarCategorias();
                if (categorias.Count > 0)
                {
                    return Ok(categorias);
                }
                return BadRequest(new
                {
                    mensaje = "No se encontraron categorías."
                });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ValidarDatosResultado")]
        public async Task<ActionResult> ValidarDatosResultado(int idCarrera, int idCompetidor, int idClub)
        {
            try
            {
                var validacion = new
                {
                    carreraExiste = false,
                    competidorExiste = false,
                    clubExiste = false,
                    nombreCarrera = "",
                    nombreCompetidor = "",
                    nombreClub = ""
                };

                // Validar carrera
                var carrera = await DaoCarrera.ObtenerCarreraPorId(idCarrera);
                if (carrera != null)
                {
                    validacion = new
                    {
                        carreraExiste = true,
                        competidorExiste = validacion.competidorExiste,
                        clubExiste = validacion.clubExiste,
                        nombreCarrera = carrera.Titulo_Carrera,
                        nombreCompetidor = validacion.nombreCompetidor,
                        nombreClub = validacion.nombreClub
                    };
                }

                // Validar competidor
                var competidor = await DaoCompetidor.ObtenerCompetidorCompletoPorIdAsync(idCompetidor);
                if (competidor != null)
                {
                    validacion = new
                    {
                        carreraExiste = validacion.carreraExiste,
                        competidorExiste = true,
                        clubExiste = validacion.clubExiste,
                        nombreCarrera = validacion.nombreCarrera,
                        nombreCompetidor = $"{competidor.Nombre} {competidor.Apellidos}",
                        nombreClub = validacion.nombreClub
                    };
                }

                // Validar club
                var club = await DaoClub.BuscarClubPorId(idClub);
                if (club != null)
                {
                    validacion = new
                    {
                        carreraExiste = validacion.carreraExiste,
                        competidorExiste = validacion.competidorExiste,
                        clubExiste = true,
                        nombreCarrera = validacion.nombreCarrera,
                        nombreCompetidor = validacion.nombreCompetidor,
                        nombreClub = club.nombre
                    };
                }

                return Ok(validacion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}
