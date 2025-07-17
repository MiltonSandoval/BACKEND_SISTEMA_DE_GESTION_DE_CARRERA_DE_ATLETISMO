namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo
{
    public class Persona
    {
        int Id_Persona { get; set; }
        string Nombre { get; set; }
        string Apellidos { get; set; }
        string Telefono { get; set; }
        DateTime Fecha_Nacimiento { get; set; }
        string Ci { get; set; }
        string Genero { get; set; }
    }
}
