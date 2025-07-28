using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data;
using MySql.Data.MySqlClient;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DAO
{
    public class DaoAdministrador
    {
        public static async Task<Administrador> Crear(Administrador administrador)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            MySqlConnection conexion = dbConnectionFactory.CrearConexion();

            try
            {
                string query = "INSERT INTO ADMINISTRADOR (ID_USUARIO, ID_PERSONA) VALUES (@idUsuario, @idPersona)";

                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idUsuario", administrador.Id_Usuario);
                cmd.Parameters.AddWithValue("@idPersona", administrador.Id_Persona);

                await cmd.ExecuteNonQueryAsync();

                // Obtener el ID generado
                cmd.CommandText = "SELECT LAST_INSERT_ID()";
                var result = await cmd.ExecuteScalarAsync();
                administrador.Id_Administrador = Convert.ToInt32(result);

                return administrador;
            }
            finally
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    await conexion.CloseAsync();
                }
                conexion.Dispose();
            }
        }
        public static async Task<List<DtoPerfilAdministrador>> ObtenerAdministradoresAsync()
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            List<DtoPerfilAdministrador> Administradores = new List<DtoPerfilAdministrador>();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "\r\nSELECT\r\n  a.ID_ADMINISTRADOR, \r\n  p.NOMBRE,\r\n  p.APELLIDOS,\r\n  p.FECHA_NACIMIENTO,\r\n  p.DOCUMENTO_IDENTIDAD,\r\n  p.TELEFONO,\r\n  p.GENERO,\r\n  p.NACIONALIDAD,\r\n  u.EMAIL_USER,\r\n  u.PASSWORD_USER, \r\n u.FECHA_REGISTRO,\r\n  u.ESTADO,\r\n  r.NOMBRE_ROL\r\nFROM administrador a \r\nLEFT JOIN persona p ON p.ID_PERSONA = a.ID_PERSONA\r\nLEFT JOIN usuario u ON u.ID_USER = a.ID_USUARIO\r\nLEFT JOIN rol r ON r.ID_ROL = u.ID_ROL\r\nWHERE r.ID_ROL = 3;\r\n";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DtoPerfilAdministrador ADMIN = new DtoPerfilAdministrador
                            (
                                int.Parse(reader["ID_ADMINISTRADOR"].ToString()),
                                reader["NOMBRE"].ToString(),
                                reader["APELLIDOS"].ToString(),
                                DateTime.Parse(reader["FECHA_NACIMIENTO"].ToString()),
                                reader["DOCUMENTO_IDENTIDAD"].ToString(),
                                reader["TELEFONO"].ToString(),
                                reader["GENERO"].ToString(),
                                reader["NACIONALIDAD"].ToString(),
                                reader["EMAIL_USER"].ToString(),
                                DateTime.Parse(reader["FECHA_REGISTRO"].ToString()),
                                bool.Parse(reader["ESTADO"].ToString()),
                                reader["PASSWORD_USER"].ToString()
                            );
                            Administradores.Add(ADMIN);
                        }
                    }
                }
            }
            return Administradores;
        }
        public static async Task<DtoPerfilAdministrador> ObtenerAdministradorPorCorreoYContra(string email, string contra)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "\r\nSELECT\r\n  a.ID_ADMINISTRADOR, \r\n  p.NOMBRE,\r\n  p.APELLIDOS,\r\n  p.FECHA_NACIMIENTO,\r\n  p.DOCUMENTO_IDENTIDAD,\r\n  p.TELEFONO,\r\n  p.GENERO,\r\n  p.NACIONALIDAD,\r\n  u.EMAIL_USER,\r\n u.PASSWORD_USER,\r\n  u.FECHA_REGISTRO,\r\n  u.ESTADO,\r\n  r.NOMBRE_ROL\r\nFROM administrador a \r\nLEFT JOIN persona p ON p.ID_PERSONA = a.ID_PERSONA\r\nLEFT JOIN usuario u ON u.ID_USER = a.ID_USUARIO\r\nLEFT JOIN rol r ON r.ID_ROL = u.ID_ROL\r\nWHERE r.ID_ROL = 3 AND u.ESTADO = 1 AND u.EMAIL_USER = @email AND u.PASSWORD_USER = @contra;\r\n";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@contra", contra);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DtoPerfilAdministrador perfilAdministrador = new DtoPerfilAdministrador
                            (
                                int.Parse(reader["ID_ADMINISTRADOR"].ToString()),
                                reader["NOMBRE"].ToString(),
                                reader["APELLIDOS"].ToString(),
                                DateTime.Parse(reader["FECHA_NACIMIENTO"].ToString()),
                                reader["DOCUMENTO_IDENTIDAD"].ToString(),
                                reader["TELEFONO"].ToString(),
                                reader["GENERO"].ToString(),
                                reader["NACIONALIDAD"].ToString(),
                                reader["EMAIL_USER"].ToString(),
                                DateTime.Parse(reader["FECHA_REGISTRO"].ToString()),
                                bool.Parse(reader["ESTADO"].ToString()),
                                reader["PASSWORD_USER"].ToString()
                            );
                            return perfilAdministrador;
                        }
                    }
                }
                return null;
            }
        }
        public static async Task<DtoPerfilAdministrador> ObtenerAdministradorPorId(int id_admin)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "\r\nSELECT\r\n  a.ID_ADMINISTRADOR, \r\n  p.NOMBRE,\r\n  p.APELLIDOS,\r\n  p.FECHA_NACIMIENTO,\r\n  p.DOCUMENTO_IDENTIDAD,\r\n  p.TELEFONO,\r\n  p.GENERO,\r\n  p.NACIONALIDAD,\r\n  u.EMAIL_USER,\r\n u.PASSWORD_USER,\r\n u.FECHA_REGISTRO,\r\n  u.ESTADO,\r\n  r.NOMBRE_ROL\r\nFROM administrador a \r\nLEFT JOIN persona p ON p.ID_PERSONA = a.ID_PERSONA\r\nLEFT JOIN usuario u ON u.ID_USER = a.ID_USUARIO\r\nLEFT JOIN rol r ON r.ID_ROL = u.ID_ROL\r\nWHERE a.ID_ADMINISTRADOR = @id;\r\n";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id_admin);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DtoPerfilAdministrador Admin = new DtoPerfilAdministrador
                            (
                                int.Parse(reader["ID_ADMINISTRADOR"].ToString()),
                                reader["NOMBRE"].ToString(),
                                reader["APELLIDOS"].ToString(),
                                DateTime.Parse(reader["FECHA_NACIMIENTO"].ToString()),
                                reader["DOCUMENTO_IDENTIDAD"].ToString(),
                                reader["TELEFONO"].ToString(),
                                reader["GENERO"].ToString(),
                                reader["NACIONALIDAD"].ToString(),
                                reader["EMAIL_USER"].ToString(),
                                DateTime.Parse(reader["FECHA_REGISTRO"].ToString()),
                                bool.Parse(reader["ESTADO"].ToString()),
                                reader["PASSWORD_USER"].ToString()
                            );
                            return Admin;
                        }
                    }
                }
            }
            return null;
        }
        public static async Task<int> ObtenerIdPersonaDeAdmin(int id_admin)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "Select ID_PERSONA from administrador WHERE ID_ADMINISTRADOR = @id_admin";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_admin", id_admin);
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
        public static async Task<int> ObtenerIdUsuarioDeAdmin(int id_admin)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "Select ID_USUARIO from administrador WHERE ID_ADMINISTRADOR = @id_admin";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_admin", id_admin);
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
        public static async Task<DtoEstadisticasAdministrador> ObtenerEstadisticas()
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                var dto = new DtoEstadisticasAdministrador();

                // Consulta para TotalCompetidores (ID_ROL = 1 y ESTADO = 1)
                string queryCompetidores = "SELECT COUNT(ID_USER) FROM USUARIO WHERE ID_ROL = 1 AND ESTADO = 1";
                await using (var cmd = new MySqlCommand(queryCompetidores, conexion))
                {
                    dto.TotalCompetidores = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                // Consulta para TotalOrganizadores (ID_ROL = 2 y ESTADO = 1)
                string queryOrganizadores = "SELECT COUNT(ID_USER) FROM USUARIO WHERE ID_ROL = 2 AND ESTADO = 1";
                await using (var cmd = new MySqlCommand(queryOrganizadores, conexion))
                {
                    dto.TotalOrganizadores = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                // Consulta para TotalCarreras (ESTADO_CARRERA = 1)
                string queryCarreras = "SELECT COUNT(ID_CARRERA) FROM CARRERA WHERE ESTADO_CARRERA = 1";
                await using (var cmd = new MySqlCommand(queryCarreras, conexion))
                {
                    dto.TotalCarreras = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                // Consulta para TotalClubes (ESTADO = 1)
                string queryClubes = "SELECT COUNT(ID_CLUB) FROM CLUB WHERE ESTADO = 1";
                await using (var cmd = new MySqlCommand(queryClubes, conexion))
                {
                    dto.TotalClubes = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                return dto;
            }
        }


    }
}