namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO
{

    public class DtoPerilUsuario
    {
        public string Nombre;
        public string Apellidos;
        public DateTime Fecha_nacimiento;
        public string Documento_Identidad;
        public string Telefono;
        public string Genero;
        public string Nacionalidad;
        public string Email;
        public string Password;
        public DateTime Fecha_Registro;
        public DtoPerilUsuario(string nombre, string apellidos, DateTime fecha_nacimiento, string documento_Identidad, string telefono, string genero, string nacionalidad, string email, string password, DateTime fecha_Registro)
        {
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
        }
    }
}
