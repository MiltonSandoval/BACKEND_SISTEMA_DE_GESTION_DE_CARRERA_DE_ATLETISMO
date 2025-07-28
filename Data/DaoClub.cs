using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DaoClub
    {
        public static async Task<Club> CrearClub(Club club)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"INSERT INTO club 
                     (NOMBRE_CLUB, REPRESENTANTE_ID, ESTADO) 
                     VALUES (@nombre, @repre, @estado )";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", club.nombre);
                    cmd.Parameters.AddWithValue("@repre", club.Representante);
                    cmd.Parameters.AddWithValue("@estado", club.estado);

                    await cmd.ExecuteNonQueryAsync();

                    long idGenerado = cmd.LastInsertedId;

                    return new Club
                    {
                        id_club = (int)idGenerado,
                        nombre = club.nombre,
                        Representante = club.Representante,
                        estado = club.estado,
                        fecha_creacion = club.fecha_creacion
                    };
                }
            }
        }
        public static async Task<DtoPerfilClub> BuscarClubPorId(int id)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            DtoPerfilClub club = new DtoPerfilClub();
            List<int> id_compe = new List<int>();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT * FROM CLUB WHERE ID_CLUB = @idclub";

                string query2 = @"SELECT ID_COMPETIDOR FROM registro_historico where ID_CLUB =@idclub";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idclub", id);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            club.id_club = (int)id;
                            club.nombre = reader["NOMBRE_CLUB"].ToString();
                            club.id_representante = int.Parse(reader["REPRESENTANTE_ID"].ToString());
                            club.estado = bool.Parse(reader["ESTADO"].ToString());
                            club.fecha_creacion = DateTime.Parse(reader["fecha_creacion"].ToString());
                        }
                    }
                }
                await using (MySqlCommand cmd = new MySqlCommand(query2, conexion))
                {
                    cmd.Parameters.AddWithValue("@idclub", id);
                    cmd.ExecuteNonQuery();
                    await using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            id_compe.Add(int.Parse(rdr["ID_COMPETIDOR"].ToString()));
                        }
                    }
                }
                foreach (var compe_jd in id_compe)
                {
                    club.competidores_del_club.Add(DaoCompetidor.ObtenerCompetidorCompletoPorIdAsync(id).Result);
                }

                return (club);
            }
        }
        public static async Task<List<Club>> ListarClubes()
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            List<Club> Listado = new List<Club>();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT * FROM CLUB";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            Club club = new Club();

                            club.id_club = int.Parse(reader["ID_CLUB"].ToString());
                            club.nombre = reader["NOMBRE_CLUB"].ToString();
                            club.Representante = int.Parse(reader["REPRESENTANTE_ID"].ToString());
                            club.estado = bool.Parse(reader["ESTADO"].ToString());
                            club.fecha_creacion = DateTime.Parse(reader["fecha_creacion"].ToString());
                            Listado.Add(club);
                        }
                    }
                }
            }
            return Listado;
        }
        public static async Task<int> ClubActualId(int id)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT ID_CLUB FROM registro_historico where ID_COMPETIDOR = @id AND FECHA_SALIDA is null";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    cmd.ExecuteNonQuery();
                    await using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            return int.Parse(rdr["ID_CLUB"].ToString());
                        }
                    }
                }
            }
            return -1;
        }
        public static async Task<List<SolicitudesClub>> SolicitudesSolicitadas(int id_club)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            List<SolicitudesClub> solicitudes = new List<SolicitudesClub>();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {   
                string query = @"SELECT * from solicitud_club where ID_ESTADO_APROBACION = 1 AND ID_CLUB = @club";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("club", id_club);
                    cmd.ExecuteNonQuery();
                    await using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            SolicitudesClub sol = new SolicitudesClub();

                            sol.id_Solicitud = int.Parse(rdr["ID_ESTADO_SOLICITUD"].ToString());
                            sol.id_club = int.Parse(rdr["ID_CLUB"].ToString());
                            sol.estado_solicitud = int.Parse(rdr["ID_ESTADO_APROBACION"].ToString());
                            sol.id_competidor = int.Parse(rdr["ID_SOLICITANTE_COMPETIDOR"].ToString());
                            solicitudes.Add(sol);
                        }
                        return solicitudes;
                    }
                }
            }
        }
        public static async Task<bool> AceptarSolicitud(SolicitudesClub soli)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"insert into registro_historico(ID_COMPETIDOR, ID_CLUB, FECHA_INGRESO) VALUES (@COMPE, @CLUB,@INGRESO);";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("COMPE", soli.id_competidor);
                    cmd.Parameters.AddWithValue("CLUB", soli.id_club);
                    cmd.Parameters.AddWithValue("INGRESO", new DateTime());

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }

        public static async Task<bool> CambiarEstadoSolicitud(int id_soli, int estado)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"update solicitud_club set ID_ESTADO_APROBACION = @estado WHERE ID_ESTADO_SOLICITUD = @id";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@estado", estado );
                    cmd.Parameters.AddWithValue("@id", id_soli);
                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }




    }
}
