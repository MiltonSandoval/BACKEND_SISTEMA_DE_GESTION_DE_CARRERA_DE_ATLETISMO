namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo
{
    public class Usuario
    {
        public int Id_User;
        public string Email;
        public string Password;
        public DateTime Fecha_Registro { get; set; }
        public bool Estado { get; set; }
        public int Id_Rol { get; set; }

    }
}
