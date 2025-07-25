using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Mapper;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Services
{
    public class SerClub
    {
        public async Task<DtoPerfilClub> RegistrarClub(DtoRegistrarClub dtoRegistroClub)
        {
            try
            {
                Club dtoPerfilClub = new Club();
                dtoPerfilClub.nombre = dtoRegistroClub.nombre;
                dtoPerfilClub.Representante = dtoRegistroClub.id_representante;
                dtoPerfilClub.estado = true;
                Club club = await DaoClub.CrearClub(dtoPerfilClub);
                DtoPerfilClub perfil = await DaoClub.BuscarClubPorId(club.id_club);

                return perfil;
            }
            catch
            {
                throw new ArgumentException($"Error: al registrar Competidor");
            }
        }

        public async Task<List<Club>> ListadoClub()
        {
            try
            {
                List<Club> Lista = await DaoClub.ListarClubes();
                return Lista;   
            }
            catch
            {
                throw new ArgumentException($"Erro inesperado al obtener listado de clubes");
            }
        }
        public async Task<List<DtoPerfilClub>> ListarDtoClubes()
        {
            try
            {

                List<Club> Lista = await DaoClub.ListarClubes();
                List<DtoPerfilClub> dtoPerfilClubs = new List<DtoPerfilClub>();
                foreach (var club in Lista)
                {
                    DtoPerfilClub dto = await DaoClub.BuscarClubPorId(club.id_club);
                    dtoPerfilClubs.Add(dto);
                }
                
                return dtoPerfilClubs;
            }
            catch
            {
                throw new ArgumentException($"Erro inesperado al obtener listado de clubes");
            }
        }
        public async Task<DtoPerfilClub> ClubActual(int id)
        {
            int idclub = DaoClub.ClubActualId(id).Result;
            if (idclub != -1)
            {
                return DaoClub.BuscarClubPorId(idclub).Result;
            }
            throw new ArgumentException("No pertenece a ningun club");
        }
        public async Task<List<SolicitudesClub>> Solicitudes(int id_club)
        {
            List<SolicitudesClub> solicitudes = await DaoClub.SolicitudesSolicitadas(id_club);
            return solicitudes;
        }
    }
}
