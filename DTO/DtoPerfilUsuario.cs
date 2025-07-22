
namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO
{


    public class DtoPerfilUsuario
    {
        public int Id_Competidor { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime Fecha_nacimiento { get; set; }
        public string Documento_Identidad { get; set; }
        public string Telefono { get; set; }
        public string Genero { get; set; }
        public string Nacionalidad { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public bool Estado { get; set; }

        public DtoPerfilUsuario(int id_competidor, string nombre, string apellidos, DateTime fecha_nacimiento, string documento_Identidad, string telefono, string genero, string nacionalidad, string email, string password, DateTime fecha_Registro, bool estado)
        {
            Id_Competidor = id_competidor;
            Nombre = nombre;
            Apellidos = apellidos;
            Fecha_nacimiento = fecha_nacimiento;
            Documento_Identidad = documento_Identidad;
            Telefono = telefono;
            Genero = genero;
            Nacionalidad = nacionalidad;
            Email = email;
            Password = password;
            Fecha_Registro = fecha_Registro;
            Estado = estado;

        }
    }
}
