namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO
{
    public class DtoPerfilAdministrador
    {
        public DtoPerfilAdministrador(int idAdministrador, string nombre, string apellidos, DateTime fechaNacimiento, string documentoIdentidad, string telefono, string genero, string nacionalidad, string email, DateTime fechaRegistro, bool estado, string password)
        {
            IdAdministrador = idAdministrador;
            Nombre = nombre;
            Apellidos = apellidos;
            FechaNacimiento = fechaNacimiento;
            DocumentoIdentidad = documentoIdentidad;
            Telefono = telefono;
            Genero = genero;
            Nacionalidad = nacionalidad;
            Email = email;
            FechaRegistro = fechaRegistro;
            Estado = estado;
            Password = password;
        }

        public DtoPerfilAdministrador() { }
        public int IdAdministrador { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Telefono { get; set; }
        public string Genero { get; set; }
        public string Nacionalidad { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
