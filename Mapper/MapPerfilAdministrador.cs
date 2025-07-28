using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Mapper
{
    public class MapPerfilAdministrador
    {
        public static Persona ObtenerPersonaDelDtoAdmin(DtoPerfilAdministrador PerfilAdmin)
        {
            Persona persona = new Persona();
            persona.Nombre = PerfilAdmin.Nombre;
            persona.Apellidos = PerfilAdmin.Apellidos;
            persona.Fecha_Nacimiento = PerfilAdmin.FechaNacimiento;
            persona.Documento_Identidad = PerfilAdmin.DocumentoIdentidad;
            persona.Telefono = PerfilAdmin.Telefono;
            persona.Genero = PerfilAdmin.Genero;
            persona.Nacionalidad = PerfilAdmin.Nacionalidad;
            return persona;
        }
        public static Usuario ObtenerUsuarioDelDtoPerfilAdmin(DtoPerfilAdministrador PerfilAdmin)
        { 
            Usuario usuario = new Usuario();
            usuario.Email = PerfilAdmin.Email;
            usuario.Password = PerfilAdmin.Password;
            usuario.Estado = PerfilAdmin.Estado;
            return usuario;
        }   
    }
}
