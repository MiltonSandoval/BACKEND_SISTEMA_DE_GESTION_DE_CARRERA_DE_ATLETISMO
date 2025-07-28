using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Services;
using Microsoft.AspNetCore.Mvc;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubController : ControllerBase
    {
        private readonly SerClub _servClub;
        public ClubController(SerClub serClub) { _servClub = serClub; }

        [HttpPost("RegistrarClub")]
        public async Task<ActionResult<DtoPerfilClub>> Post([FromBody] DtoRegistrarClub dtoRegistroClub)
        {
            try
            {
                DtoPerfilClub Club = await _servClub.RegistrarClub(dtoRegistroClub);
                if (!(Club is null))
                {
                    return Ok(Club);
                }
                else
                {
                    return BadRequest(new
                    {
                        mensaje = "Club no encontrado."
                    });
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ListarClubes")]
        public async Task<ActionResult<List<Club>>> GetClubes()
        {
            try
            {
                List<Club> ListadoClub =  await _servClub.ListadoClub();
                if (ListadoClub.Count > 0)
                {
                    return Ok(ListadoClub);
                }
                return BadRequest
                    (
                        new
                        {
                            mensaje = "No han reguistrado ningun club"
                        });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ListarPerfilCLubes")]
        public async Task<ActionResult<List<DtoPerfilClub>>> GetPerfilCLubes()
        {
            try
            {
                List<DtoPerfilClub> ListadoClub = await _servClub.ListarDtoClubes();
                if (ListadoClub.Count > 0)
                {
                    return Ok(ListadoClub);
                }
                return BadRequest
                    (
                        new
                        {
                            mensaje = "No han reguistrado ningun club"
                        });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ClubPerteneciente")]
        public async Task<ActionResult<DtoPerfilClub>> GetClubPerteneciente( int id)
        {
            try
            {

                DtoPerfilClub ClubActual = await _servClub.ClubActual( id);
                if (ClubActual != null)
                    return Ok(ClubActual);
                return BadRequest(new { mensaje = "No se a inscrito a un club" });
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
        [HttpGet("ObtenerSolicitudesClub/")]

        public async Task<ActionResult<List<SolicitudesClub>>> GetSolicitudes(int id_club)
        {
            try
            {

                List<SolicitudesClub> Solicitudes = await _servClub.Solicitudes(id_club);
                return Ok(Solicitudes);

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("AceptarSolicitud/")]
        public async Task<ActionResult<DtoPerfilClub>> PostAceptarClub(SolicitudesClub solicitud)
        {
            try
            {
                var club = _servClub.ClubActual(solicitud.id_competidor).Result;
                if (club == null && club.)
                {
                    bool Solicitud = SerClub.aceptarSoli(solicitud).Result;
                    if (Solicitud)
                        return Ok(_servClub.ClubActual(solicitud.id_competidor).Result);
                    return BadRequest("Error al unirse al club");
                }
                else
                {
                    return BadRequest("Error ya perteces a un club");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message );
            }
        }
    }
}
