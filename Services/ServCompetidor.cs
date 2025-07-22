using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Mapper;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Services
{
    public class ServCompetidor
    {
        public List<DtoPerfilUsuario> ObtenerCompetidoresActivos()
        {   
            List<DtoPerfilUsuario> Competidores =  DaoCompetidor.ObtenerCompetidoresActivosAsync();
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
                await DaoCompetidor.InsertarCompetidor(new Competidor
                {
                    Id_Persona = await DaoPersona.ObtenerIdPersonaPorDocumentoAsyn(persona.Documento_Identidad),
                    Id_Usuario = await DaoUsuario.ObtenerIdUsuarioPorEmail(usuario.Email)
                });
                    
                return DaoCompetidor.ObtenerCompetidorPorIdPersonaAsync(persona.Id_Persona).Result;
            }
            catch
            {
                throw new ArgumentException($"Error: al registrar Competidor");
            }
        }
    }
}
