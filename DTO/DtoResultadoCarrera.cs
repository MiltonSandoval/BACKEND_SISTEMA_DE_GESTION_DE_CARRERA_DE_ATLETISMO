namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO
{
    public class DtoResultadoCarrera
    {
        public int Id_Carrera { get; set; }
        public int Id_Competidor { get; set; }
        public int Id_Club { get; set; }
        public int Puntuacion { get; set; }
        public string Tiempo { get; set; } // CAMBIAR DE TimeSpan a string
        public int Posicion { get; set; }
        public string Nombre_Competidor { get; set; }
        public string Nombre_Club { get; set; }

        public DtoResultadoCarrera() { }

        public DtoResultadoCarrera(int id_carrera, int id_competidor, int id_club, int puntuacion, string tiempo, int posicion, string nombre_competidor, string nombre_club)
        {
            Id_Carrera = id_carrera;
            Id_Competidor = id_competidor;
            Id_Club = id_club;
            Puntuacion = puntuacion;
            Tiempo = tiempo; // CAMBIAR aquí también
            Posicion = posicion;
            Nombre_Competidor = nombre_competidor;
            Nombre_Club = nombre_club;
        }
    }
}