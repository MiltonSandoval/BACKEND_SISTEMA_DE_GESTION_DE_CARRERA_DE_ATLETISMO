namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo
{
    public class Categoria
    {
        public int Id_Categoria { get; set; }
        public string Nombre_Categoria { get; set; }
        public int Edad_Minima { get; set; }
        public int Edad_Maxima { get; set; }
        public string Genero { get; set; }
        public int Distancia { get; set; }
    }
}