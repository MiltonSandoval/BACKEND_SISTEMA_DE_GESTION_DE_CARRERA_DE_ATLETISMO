namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo
{
    public class Carrera
    {
        public int Id_Carrera { get; set; }
        public int Id_Organizador { get; set; }
        public int Id_Categoria { get; set; }
        public string Titulo_Carrera { get; set; }
        public DateTime Fecha_Inicio_Inscripcion { get; set; }
        public DateTime Fecha_Fin_Inscripcion { get; set; }
        public string Lugar { get; set; }
        public int Cantidad_Maxima_Participante { get; set; }
        public bool Estado_Carrera { get; set; }
        public decimal Precio_Inscripcion { get; set; }
        public string Nombre_Categoria { get; set; }
        public string Nombre_Organizador { get; set; }
    }
}