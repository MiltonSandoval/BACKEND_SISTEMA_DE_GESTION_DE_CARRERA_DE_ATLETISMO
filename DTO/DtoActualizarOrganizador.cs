namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO
{
    public class DtoActualizarOrganizador
    {
        public int Id_Organizador { get; set; }  // Para identificar cuál actualizar
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public DtoActualizarOrganizador() { }

        public DtoActualizarOrganizador(int id_organizador, string nombre, string apellidos, string telefono, string email)
        {
            Id_Organizador = id_organizador;
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = telefono;
            Email = email;
        }
    }
}