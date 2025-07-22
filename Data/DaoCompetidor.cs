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
                string query = "\r\nselect\r\nc.ID_COMPETIDOR, \r\np.NOMBRE,\r\np.APELLIDOS,\r\np.FECHA_NACIMIENTO,\r\np.DOCUMENTO_IDENTIDAD,\r\np.TELEFONO,\r\np.GENERO,\r\np.NACIONALIDAD,\r\nu.EMAIL_USER,\r\nu.PASSWORD_USER,\r\nu.FECHA_REGISTRO,\r\nu.ESTADO,\r\nr.NOMBRE_ROL\r\nFROM competidor c \r\nJOIN persona p on p.ID_PERSONA = c.ID_PERSONA\r\nJOIN usuario u ON u.ID_USER = c.ID_USUARIO\r\nJOIN rol r on r.ID_ROL = u.ID_ROL\r\nWHERE u.ESTADO = 1\r\nAND c.ID_COMPETIDOR = @id";

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


        public static List<DtoPerfilUsuario> ObtenerCompetidoresActivosAsync()
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            List<DtoPerfilUsuario> competidores = new List<DtoPerfilUsuario>();

            using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "select\r\nc.ID_COMPETIDOR, \r\np.NOMBRE,\r\np.APELLIDOS,\r\np.FECHA_NACIMIENTO,\r\np.DOCUMENTO_IDENTIDAD,\r\np.TELEFONO,\r\np.GENERO,\r\np.NACIONALIDAD,\r\nu.EMAIL_USER,\r\nu.PASSWORD_USER,\r\nu.FECHA_REGISTRO,\r\nu.ESTADO,\r\nr.NOMBRE_ROL, \r\n u.ESTADO\r\nFROM competidor c \r\nJOIN persona p on p.ID_PERSONA = c.ID_PERSONA\r\nJOIN usuario u ON u.ID_USER = c.ID_USUARIO\r\nJOIN rol r on r.ID_ROL = u.ID_ROL\r\nWHERE u.ESTADO = 1\r\nAND r.ID_ROL = 1\r\n;\r\n";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
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
        
    }
}
