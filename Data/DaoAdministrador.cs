using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data;
using MySql.Data.MySqlClient;
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

                conexion.Close();
                return administrador;
            }
            finally
            {
                conexion.Close();
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    await conexion.CloseAsync();
                }
                conexion.Dispose();
            }
        }
    }
}