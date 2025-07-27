namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO
{
    public class DtoCambiarPassword
    {
        public int Id_Organizador { get; set; }
        public string PasswordActual { get; set; }
        public string PasswordNuevo { get; set; }

        public DtoCambiarPassword() { }

        public DtoCambiarPassword(int id_organizador, string passwordActual, string passwordNuevo)
        {
            Id_Organizador = id_organizador;
            PasswordActual = passwordActual;
            PasswordNuevo = passwordNuevo;
        }
    }
}