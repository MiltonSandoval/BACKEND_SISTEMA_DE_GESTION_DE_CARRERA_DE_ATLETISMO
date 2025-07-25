using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Mapper;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Services
{
    public class ServCompetidor
    {
        public List<DtoPerfilUsuario> ObtenerCompetidoresActivos()
        {   
            List<DtoPerfilUsuario> Competidores =  DaoCompetidor.ObtenerCompetidoresActivosAsync().Result;
            if (Competidores.Count < 0)
            {
                throw new ArgumentException("No hay competidores registrados");
            }
            return Competidores;

        }

        public async Task<DtoPerfilUsuario> RegistrarCompetidor(DtoRegistroUsuario dtoRegistroUsuario)
        {
            try
            {
                Persona persona = MapPerfilUsuario.ObtenerPersonaDelDtoRegistro(dtoRegistroUsuario);
                Usuario usuario = MapPerfilUsuario.ObtenerUsuarioDelDtoRegistro(dtoRegistroUsuario);
                usuario.Estado = true;
                usuario.Id_Rol = 1; // Asignar rol de competidor
                await DaoPersona.CrearPersonaAsync(persona);
                await DaoUsuario.CrearUsuarioAsync(usuario);
                Competidor compe = new Competidor
                {
                    Id_Persona = await DaoPersona.ObtenerIdPersonaPorDocumentoAsyn(persona.Documento_Identidad),
                    Id_Usuario = await DaoUsuario.ObtenerIdUsuarioPorEmail(usuario.Email)
                };
                await DaoCompetidor.InsertarCompetidor(compe);
                    
                return DaoCompetidor.ObtenerCompetidorPorIdPersonaAsync(compe.Id_Persona).Result;
            }
            catch
            {
                throw new ArgumentException($"Error: al registrar Competidor");
            }
        }

        public async Task<DtoPerfilUsuario> LoginCompetidor(string Email, string Password)
        {
            DtoPerfilUsuario perfilCompetidor = await DaoCompetidor.ObtenerCompetidorPorCorreoYContra(Email, Password);
            if (perfilCompetidor is null)
            {
                throw new ArgumentException("Credenciales incorrectas");
            }
            return perfilCompetidor;
        }
        public async Task<DtoPerfilUsuario> ActualizarEstadoCompetidor(int id)
        {
            try
            {
                bool resultado = await DaoUsuario.CambiarEstadoAsyn(await DaoCompetidor.ObtenerIdUsuarioDeCompetidor(id));
                if (resultado)
                {
                    DtoPerfilUsuario competidor =  DaoCompetidor.ObtenerCompetidorCompletoPorIdAsync(id).Result;
                    return  competidor;
                }
                else
                {
                    throw new ArgumentException("Error al actualizar el estado del competidor");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<DtoPerfilUsuario> ActualizarPerfilCompetidor(DtoPerfilUsuario Competidor)
        {
            try
            {

                Usuario user =  MapPerfilUsuario.ObtenerUsuarioDelDtoPerfil(Competidor);
                user.Id_User = await DaoCompetidor.ObtenerIdUsuarioDeCompetidor(Competidor.Id_Competidor);
                Persona Peaople = MapPerfilUsuario.ObtenerPersonaDelDtoPerfil(Competidor);
                Peaople.Id_Persona = await DaoCompetidor.ObtenerIdPersonaDeCompetidor(Competidor.Id_Competidor);

                await DaoPersona.ActualizarPersona(Peaople);
                await DaoUsuario.ActualizarUsuario(user);
                DtoPerfilUsuario CompetidorActualizado = DaoCompetidor.ObtenerCompetidorCompletoPorIdAsync(Competidor.Id_Competidor).Result;
                return Competidor;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  async Task<bool> SolicitarUnirseClub(int id_club, int id_competidor)
        {
            try
            {
                int Verificador = await DaoClub.ClubActualId(id_competidor);
                if (Verificador == -1)
                {
                    bool Solicitado = await DaoCompetidor.CrearSolicitud(id_club, id_competidor);
                    return Solicitado;

                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
