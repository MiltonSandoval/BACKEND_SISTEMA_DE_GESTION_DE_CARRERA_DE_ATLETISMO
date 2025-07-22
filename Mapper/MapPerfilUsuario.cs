using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Mapper
{
    public class MapPerfilUsuario
    {

        public static Persona ObtenerPersonaDelDtoRegistro(DtoRegistroUsuario perfilusuario)
        {
            Persona persona = new Persona();
            persona.Nombre = perfilusuario.Nombre;
            persona.Apellidos = perfilusuario.Apellidos;
            persona.Fecha_Nacimiento = perfilusuario.Fecha_nacimiento;
            persona.Documento_Identidad = perfilusuario.Documento_Identidad;
            persona.Telefono = perfilusuario.Telefono;
            persona.Genero = perfilusuario.Genero;
            persona.Nacionalidad = perfilusuario.Nacionalidad;
            return persona;
        }
        public static Usuario ObtenerUsuarioDelDtoRegistro(DtoRegistroUsuario perfilusuario)
        {
            Usuario usuario = new Usuario(); 
            usuario.Email = perfilusuario.Email;
            usuario.Password = perfilusuario.Password;

            return usuario;
        }

        
    }
}
