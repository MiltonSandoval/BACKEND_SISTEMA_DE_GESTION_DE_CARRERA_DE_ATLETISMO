using System.ComponentModel;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DaoCompetidor
    {
        public static async Task<bool> InsertarCompetidor(Competidor competidor)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {

                string query = "INSERT INTO competidor (ID_PERSONA, ID_USUARIO) VALUES (@persona, @usuario)";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@persona", competidor.Id_Persona);
                    cmd.Parameters.AddWithValue("@usuario", competidor.Id_Usuario);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }
        public static async Task<DtoPerfilUsuario> ObtenerCompetidorPorIdPersonaAsync(int idpersona)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "select\r\nc.ID_COMPETIDOR, \r\np.NOMBRE,\r\np.APELLIDOS,\r\np.FECHA_NACIMIENTO,\r\np.DOCUMENTO_IDENTIDAD,\r\np.TELEFONO,\r\np.GENERO,\r\np.NACIONALIDAD,\r\nu.EMAIL_USER,\r\nu.PASSWORD_USER,\r\nu.FECHA_REGISTRO,\r\nu.ESTADO,\r\nr.NOMBRE_ROL\r\nFROM competidor c \r\nJOIN persona p on p.ID_PERSONA = c.ID_PERSONA\r\nJOIN usuario u ON u.ID_USER = c.ID_USUARIO\r\nJOIN rol r on r.ID_ROL = u.ID_ROL\r\nWHERE u.ESTADO = 1\r\nAND p.ID_PERSONA = @id";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idpersona);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DtoPerfilUsuario PerfilCompetidor = new DtoPerfilUsuario
                            (
                                int.Parse(reader["ID_COMPETIDOR"].ToString()),
                                reader["NOMBRE"].ToString(),
                                reader["APELLIDOS"].ToString(),
                                DateTime.Parse(reader["FECHA_NACIMIENTO"].ToString()),
                                reader["DOCUMENTO_IDENTIDAD"].ToString(),
                                reader["TELEFONO"].ToString(),
                                reader["GENERO"].ToString(),
                                reader["NACIONALIDAD"].ToString(),
                                reader["EMAIL_USER"].ToString(),
                                reader["PASSWORD_USER"].ToString(),
                                DateTime.Parse(reader["FECHA_REGISTRO"].ToString()),
                                bool.Parse(reader["ESTADO"].ToString())
                            );
                            return PerfilCompetidor;
                        }
                    }
                }
            }
            return null;
        }
        public static async Task<DtoPerfilUsuario> ObtenerCompetidorCompletoPorIdAsync(int idcompetidor)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "\r\nselect\r\nc.ID_COMPETIDOR, \r\np.NOMBRE,\r\np.APELLIDOS,\r\np.FECHA_NACIMIENTO,\r\np.DOCUMENTO_IDENTIDAD,\r\np.TELEFONO,\r\np.GENERO,\r\np.NACIONALIDAD,\r\nu.EMAIL_USER,\r\nu.PASSWORD_USER,\r\nu.FECHA_REGISTRO,\r\nu.ESTADO,\r\nr.NOMBRE_ROL\r\nFROM competidor c \r\nJOIN persona p on p.ID_PERSONA = c.ID_PERSONA\r\nJOIN usuario u ON u.ID_USER = c.ID_USUARIO\r\nJOIN rol r on r.ID_ROL = u.ID_ROL\r\nAND c.ID_COMPETIDOR = @id";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idcompetidor);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DtoPerfilUsuario PerfilCompetidor = new DtoPerfilUsuario
                            (
                                int.Parse(reader["ID_COMPETIDOR"].ToString()),
                                reader["NOMBRE"].ToString(),
                                reader["APELLIDOS"].ToString(),
                                DateTime.Parse(reader["FECHA_NACIMIENTO"].ToString()),
                                reader["DOCUMENTO_IDENTIDAD"].ToString(),
                                reader["TELEFONO"].ToString(),
                                reader["GENERO"].ToString(),
                                reader["NACIONALIDAD"].ToString(),
                                reader["EMAIL_USER"].ToString(),
                                reader["PASSWORD_USER"].ToString(),
                                DateTime.Parse(reader["FECHA_REGISTRO"].ToString()),
                                bool.Parse(reader["ESTADO"].ToString())
                            );
                            return PerfilCompetidor;
                        }
                    }
                }
            }
            return null;
        }


        public static async Task<List<DtoPerfilUsuario>> ObtenerCompetidoresActivosAsync()
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            List<DtoPerfilUsuario> competidores = new List<DtoPerfilUsuario>();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "select\r\nc.ID_COMPETIDOR, \r\np.NOMBRE,\r\np.APELLIDOS,\r\np.FECHA_NACIMIENTO,\r\np.DOCUMENTO_IDENTIDAD,\r\np.TELEFONO,\r\np.GENERO,\r\np.NACIONALIDAD,\r\nu.EMAIL_USER,\r\nu.PASSWORD_USER,\r\nu.FECHA_REGISTRO,\r\nu.ESTADO,\r\nr.NOMBRE_ROL, \r\n u.ESTADO\r\nFROM competidor c \r\nJOIN persona p on p.ID_PERSONA = c.ID_PERSONA\r\nJOIN usuario u ON u.ID_USER = c.ID_USUARIO\r\nJOIN rol r on r.ID_ROL = u.ID_ROL\r\nWHERE u.ESTADO = 1\r\nAND r.ID_ROL = 1\r\n;\r\n";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DtoPerfilUsuario UsuarioCompe = new DtoPerfilUsuario
                            (
                                int.Parse(reader["ID_COMPETIDOR"].ToString()),
                                reader["NOMBRE"].ToString(),
                                reader["APELLIDOS"].ToString(),
                                DateTime.Parse(reader["FECHA_NACIMIENTO"].ToString()),
                                reader["DOCUMENTO_IDENTIDAD"].ToString(),
                                reader["TELEFONO"].ToString(),
                                reader["GENERO"].ToString(),
                                reader["NACIONALIDAD"].ToString(),
                                reader["EMAIL_USER"].ToString(),
                                reader["PASSWORD_USER"].ToString(),
                                DateTime.Parse(reader["FECHA_REGISTRO"].ToString()),
                                bool.Parse(reader["ESTADO"].ToString())
                            );
                            competidores.Add(UsuarioCompe);
                        }
                    }
                }
            }
            return competidores;
        }
        public static async Task<DtoPerfilUsuario> ObtenerCompetidorPorCorreoYContra(string email, string contra)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "select\r\nc.ID_COMPETIDOR, \r\np.NOMBRE,\r\np.APELLIDOS,\r\np.FECHA_NACIMIENTO,\r\np.DOCUMENTO_IDENTIDAD,\r\np.TELEFONO,\r\np.GENERO,\r\np.NACIONALIDAD,\r\nu.EMAIL_USER,\r\nu.PASSWORD_USER,\r\nu.FECHA_REGISTRO,\r\nu.ESTADO,\r\nr.NOMBRE_ROL\r\nFROM competidor c \r\nJOIN persona p on p.ID_PERSONA = c.ID_PERSONA\r\nJOIN usuario u ON u.ID_USER = c.ID_USUARIO\r\nJOIN rol r on r.ID_ROL = u.ID_ROL\r\nWHERE u.ESTADO = 1\r\nAND u.EMAIL_USER = @email\r\nAND u.PASSWORD_USER = @contra";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@contra", contra);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DtoPerfilUsuario PerfilCompetidor = new DtoPerfilUsuario
                            (
                                int.Parse(reader["ID_COMPETIDOR"].ToString()),
                                reader["NOMBRE"].ToString(),
                                reader["APELLIDOS"].ToString(),
                                DateTime.Parse(reader["FECHA_NACIMIENTO"].ToString()),
                                reader["DOCUMENTO_IDENTIDAD"].ToString(),
                                reader["TELEFONO"].ToString(),
                                reader["GENERO"].ToString(),
                                reader["NACIONALIDAD"].ToString(),
                                reader["EMAIL_USER"].ToString(),
                                reader["PASSWORD_USER"].ToString(),
                                DateTime.Parse(reader["FECHA_REGISTRO"].ToString()),
                                bool.Parse(reader["ESTADO"].ToString())
                            );
                            return PerfilCompetidor;
                        }
                    }
                }
                return null;
            }
        }
        public static async Task<int> ObtenerIdUsuarioDeCompetidor(int Id_Competidor)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "Select ID_USUARIO from COMPETIDOR WHERE ID_COMPETIDOR = @id_compe";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_compe", Id_Competidor);
                    cmd.ExecuteNonQuery();
                    await using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return int.Parse(reader["ID_USUARIO"].ToString());
                        }
                    }
                }
            }
            return -1;
        }
        public static async Task<int> ObtenerIdPersonaDeCompetidor(int Id_Competidor)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "Select ID_COMPETIDOR from COMPETIDOR WHERE ID_COMPETIDOR = @id_compe";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_compe", Id_Competidor);
                    cmd.ExecuteNonQuery();
                    await using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return int.Parse(reader["ID_COMPETIDOR"].ToString());
                        }
                    }
                }
            }
            return -1;
        }
        public static async Task<bool> CrearSolicitud(int id_club, int id_competidor)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "insert into solicitud_club (ID_CLUB, ID_ESTADO_APROBACION, ID_SOLICITANTE_COMPETIDOR)  value(@club, @estado, @compe)";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@club", id_club);
                    cmd.Parameters.AddWithValue("@estado", 1);
                    cmd.Parameters.AddWithValue("@compe", id_competidor);
                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }

    }
}
