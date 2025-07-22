namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo
{
    public class Usuario
    {
        public int Id_User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public bool Estado { get; set; }
        public int Id_Rol { get; set; }

    }
}
