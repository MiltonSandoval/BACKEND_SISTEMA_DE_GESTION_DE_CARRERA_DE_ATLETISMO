namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo
{
    public class Persona
    {
        public int Id_Persona { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Documento_Identidad { get; set; }
        public string Genero { get; set; }
        public string Nacionalidad { get; set; }
    }
}
