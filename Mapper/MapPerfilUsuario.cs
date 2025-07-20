using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Mapper
{
    public class MapPerfilUsuario
    {

        static Persona ObtenerPersonaDelDto(DtoPerilUsuario perfilusuario)
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
        static Usuario ObtenerUsuarioDelDto(DtoPerilUsuario perfilusuario)
        {
            Usuario usuario = new Usuario(); 
            usuario.Email = perfilusuario.Email;
            usuario.Password = perfilusuario.Password;
            usuario.Fecha_Registro = perfilusuario.Fecha_Registro;
            return usuario;
        }

        static DtoPerilUsuario GenerarDtoPerfil(Persona persona, Usuario usuario)
        {
            return new DtoPerilUsuario(
                persona.Nombre,
                persona.Apellidos,
                persona.Fecha_Nacimiento,
                persona.Documento_Identidad,
                persona.Telefono,
                persona.Genero,
                persona.Nacionalidad,
                usuario.Email,
                usuario.Password,
                usuario.Fecha_Registro
            );
        }
    }
}
