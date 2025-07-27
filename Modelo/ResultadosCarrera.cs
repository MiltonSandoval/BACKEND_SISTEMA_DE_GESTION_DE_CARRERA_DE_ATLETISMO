namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo
{
    public class ResultadoCarrera
    {
        public int Id_Carrera { get; set; }
        public int Id_Competidor { get; set; }
        public int Id_Club { get; set; }
        public int Puntuacion { get; set; }
        public TimeSpan Tiempo { get; set; }
        public int Posicion { get; set; }
    }
}