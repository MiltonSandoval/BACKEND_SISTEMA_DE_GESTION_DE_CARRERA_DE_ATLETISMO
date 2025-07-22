
using System.Diagnostics.Contracts;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;
namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DaoUsuario
    {
        public static async Task<bool> CrearUsuarioAsync(Usuario usuario)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "INSERT INTO USUARIO (EMAIL_USER, PASSWORD_USER,ESTADO, ID_ROL) VALUES (@correo, @contra,@estado, @rol)"; 
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@correo", usuario.Email);
                    cmd.Parameters.AddWithValue("@contra", usuario.Password);
                    cmd.Parameters.AddWithValue("@estado", usuario.Estado);
                    cmd.Parameters.AddWithValue("@rol", usuario.Id_Rol);

                    return (cmd.ExecuteNonQuery() > 0);    
                }
            }
        }

        static async Task<Usuario> ObtenerUsuarioPorIdAsyn(int idUsuario)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "SELECT * FROM USUARIO WHERE ID_USER = @usuario";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", idUsuario);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Usuario usuario = new Usuario();

                            usuario.Id_User = int.Parse(reader["ID_USER"].ToString());
                            usuario.Email = reader["EMAIL_USER"].ToString();
                            usuario.Password = reader["PASSWORD_USER"].ToString();
                            usuario.Fecha_Registro = DateTime.Parse(reader["FECHA_REGISTRO"].ToString());
                            usuario.Estado = bool.Parse(reader["ESTADO"].ToString());
                            usuario.Id_Rol = int.Parse(reader["ID_ROL"].ToString());

                            return usuario;
                        }
                    }
                }
            }
            return null;
        }

        public static async Task<int> ObtenerIdUsuarioPorEmail(string Correo)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "SELECT * FROM USUARIO WHERE EMAIL_USER = @correo";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@correo", Correo);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Usuario usuario = new Usuario();

                            usuario.Id_User = int.Parse(reader["ID_USER"].ToString());
                            
                            return usuario.Id_User;
                        }
                    }
                }
            }
            return -1;
        }

        static async Task<Usuario> ObtenerUsuarioPorCorreoYContraAsync(string Correo, string Contra)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "SELECT * FROM USUARIO WHERE EMAIL_USER = @correo AND PASSWORD_USER = @contra";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@correo", Correo);
                    cmd.Parameters.AddWithValue("@contra", Contra);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader =  cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Usuario usuario = new Usuario();

                            usuario.Id_User = int.Parse(reader["ID_USER"].ToString());
                            usuario.Email = reader["EMAIL_USER"].ToString();
                            usuario.Password = reader["PASSWORD_USER"].ToString();
                            usuario.Fecha_Registro = DateTime.Parse(reader["FECHA_REGISTRO"].ToString());
                            usuario.Estado = bool.Parse(reader["ESTADO"].ToString());
                            usuario.Id_Rol = int.Parse(reader["ID_ROL"].ToString());

                            return usuario;
                        }
                    }
                }
            }
            return null;
        }

        static async Task<bool> CambiarContrasenaAsyn(int Correo, string nuevaContrasena, string contrasenaAntigua) 
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "UPDATE USUARIO SET PASSWORD_USER = @nuevaContra WHERE EMAIL_USER = @correo and PASSWORD_USER = @contraAntigua";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nuevaContra", nuevaContrasena);
                    cmd.Parameters.AddWithValue("@correo", Correo);
                    cmd.Parameters.AddWithValue("@contraAntigua", contrasenaAntigua);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }
        static async Task<bool> CambiarEstadoAsyn(int idUsuario, bool nuevoEstado)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "UPDATE USUARIO SET ESTADO = @nuevoEstado WHERE ID_USER = @idUsuario";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }

        static async Task<bool> EliminarUsuario(int idUsuario)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "DELETE FROM USUARIO WHERE ID_USER = @idUsuario";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }
    }

}
