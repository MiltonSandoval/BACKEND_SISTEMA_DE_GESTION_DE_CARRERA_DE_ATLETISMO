using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DaoOrganizador
    {
        public static async Task<bool> CrearOrganizador(Organizador organizador)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "INSERT INTO ORGANIZADOR (ID_PERSONA, ID_USUARIO) VALUES (@persona, @usuario)";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@persona", organizador.Id_Persona);
                    cmd.Parameters.AddWithValue("@usuario", organizador.Id_Usuario);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }

        public static async Task<DtoPerfilOrganizador> ObtenerOrganizadorPorIdPersonaAsync(int idpersona)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT 
                    o.ID_ORGANIZADOR, 
                    p.NOMBRE,
                    p.APELLIDOS,
                    p.FECHA_NACIMIENTO,
                    p.DOCUMENTO_IDENTIDAD,
                    p.TELEFONO,
                    p.GENERO,
                    p.NACIONALIDAD,
                    u.EMAIL_USER,
                    u.PASSWORD_USER,
                    u.FECHA_REGISTRO,
                    u.ESTADO,
                    r.NOMBRE_ROL
                    FROM ORGANIZADOR o 
                    JOIN PERSONA p ON p.ID_PERSONA = o.ID_PERSONA
                    JOIN USUARIO u ON u.ID_USER = o.ID_USUARIO
                    JOIN ROL r ON r.ID_ROL = u.ID_ROL
                    WHERE u.ESTADO = 1
                    AND p.ID_PERSONA = @id";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idpersona);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DtoPerfilOrganizador perfilOrganizador = new DtoPerfilOrganizador
                            (
                                int.Parse(reader["ID_ORGANIZADOR"].ToString()),
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
                            return perfilOrganizador;
                        }
                    }
                }
            }
            return null;
        }

        public static async Task<DtoPerfilOrganizador> ObtenerOrganizadorCompletoPorIdAsync(int idorganizador)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT 
                    o.ID_ORGANIZADOR, 
                    p.NOMBRE,
                    p.APELLIDOS,
                    p.FECHA_NACIMIENTO,
                    p.DOCUMENTO_IDENTIDAD,
                    p.TELEFONO,
                    p.GENERO,
                    p.NACIONALIDAD,
                    u.EMAIL_USER,
                    u.PASSWORD_USER,
                    u.FECHA_REGISTRO,
                    u.ESTADO,
                    r.NOMBRE_ROL
                    FROM ORGANIZADOR o 
                    JOIN PERSONA p ON p.ID_PERSONA = o.ID_PERSONA
                    JOIN USUARIO u ON u.ID_USER = o.ID_USUARIO
                    JOIN ROL r ON r.ID_ROL = u.ID_ROL
                    WHERE o.ID_ORGANIZADOR = @id";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idorganizador);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DtoPerfilOrganizador perfilOrganizador = new DtoPerfilOrganizador
                            (
                                int.Parse(reader["ID_ORGANIZADOR"].ToString()),
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
                            return perfilOrganizador;
                        }
                    }
                }
            }
            return null;
        }

        public static async Task<DtoPerfilOrganizador> ObtenerOrganizadorPorCorreoYContra(string email, string contra)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT 
                    o.ID_ORGANIZADOR, 
                    p.NOMBRE,
                    p.APELLIDOS,
                    p.FECHA_NACIMIENTO,
                    p.DOCUMENTO_IDENTIDAD,
                    p.TELEFONO,
                    p.GENERO,
                    p.NACIONALIDAD,
                    u.EMAIL_USER,
                    u.PASSWORD_USER,
                    u.FECHA_REGISTRO,
                    u.ESTADO,
                    r.NOMBRE_ROL
                    FROM ORGANIZADOR o 
                    JOIN PERSONA p ON p.ID_PERSONA = o.ID_PERSONA
                    JOIN USUARIO u ON u.ID_USER = o.ID_USUARIO
                    JOIN ROL r ON r.ID_ROL = u.ID_ROL
                    WHERE u.ESTADO = 1
                    AND u.EMAIL_USER = @email
                    AND u.PASSWORD_USER = @contra";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@contra", contra);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DtoPerfilOrganizador perfilOrganizador = new DtoPerfilOrganizador
                            (
                                int.Parse(reader["ID_ORGANIZADOR"].ToString()),
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
                            return perfilOrganizador;
                        }
                    }
                }
                return null;
            }
        }

        public static async Task<int> ObtenerIdUsuarioDeOrganizador(int Id_Organizador)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "SELECT ID_USUARIO FROM ORGANIZADOR WHERE ID_ORGANIZADOR = @id_org";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_org", Id_Organizador);
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

        public static async Task<int> ObtenerIdPersonaDeOrganizador(int Id_Organizador)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "SELECT ID_PERSONA FROM ORGANIZADOR WHERE ID_ORGANIZADOR = @id_org";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_org", Id_Organizador);
                    cmd.ExecuteNonQuery();
                    await using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return int.Parse(reader["ID_PERSONA"].ToString());
                        }
                    }
                }
            }
            return -1;
        }
    }
}
