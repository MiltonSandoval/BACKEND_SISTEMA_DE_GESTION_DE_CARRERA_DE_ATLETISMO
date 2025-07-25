namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO
{
    public class DtoPerfilClub
    {
        public int id_club { get; set; }
        public string nombre { get; set; }
        public int id_representante { get; set; }
        public DateTime fecha_creacion { get; set; }
        public bool estado {  get; set; }
        public List<DtoPerfilUsuario> competidores_del_club { get; set; }
    }
}
