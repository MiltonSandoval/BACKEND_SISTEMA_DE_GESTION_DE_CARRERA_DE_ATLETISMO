namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo
{
    public class Usuario
    {
        int Id_User { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        DateTime Fecha_Registro { get; set; }
        bool Estado { get; set; }
        int Id_Rol { get; set; }

    }
}
