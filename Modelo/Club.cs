namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo
{
    public class Club
    {
        public int id_club {  get; set; }
        public string nombre { get; set; }
        public int Representante { get; set; }
        public bool estado { get; set; }
        public DateTime fecha_creacion {  get; set; }
    }

}
