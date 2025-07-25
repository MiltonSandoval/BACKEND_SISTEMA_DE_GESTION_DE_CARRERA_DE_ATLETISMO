namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo
{
    public class SolicitudesClub
    {
        public int id_Solicitud {  get; set; }
        public int id_club { get; set; }
        public int estado_solicitud {  get; set; }
        public int id_competidor { get; set; }
    }
}
