namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO
{
    public class DtoCrearCarrera
    {
        public int Id_Organizador { get; set; }
        public int Id_Categoria { get; set; }
        public string Titulo_Carrera { get; set; }
        public DateTime Fecha_Inicio_Inscripcion { get; set; }
        public DateTime Fecha_Fin_Inscripcion { get; set; }
        public string Lugar { get; set; }
        public int Cantidad_Maxima_Participante { get; set; }
        public decimal Precio_Inscripcion { get; set; }

        public DtoCrearCarrera() { }

        public DtoCrearCarrera(int id_organizador, int id_categoria, string titulo_carrera, DateTime fecha_inicio, DateTime fecha_fin, string lugar, int cantidad_maxima, decimal precio)
        {
            Id_Organizador = id_organizador;
            Id_Categoria = id_categoria;
            Titulo_Carrera = titulo_carrera;
            Fecha_Inicio_Inscripcion = fecha_inicio;
            Fecha_Fin_Inscripcion = fecha_fin;
            Lugar = lugar;
            Cantidad_Maxima_Participante = cantidad_maxima;
            Precio_Inscripcion = precio;
        }
    }
}