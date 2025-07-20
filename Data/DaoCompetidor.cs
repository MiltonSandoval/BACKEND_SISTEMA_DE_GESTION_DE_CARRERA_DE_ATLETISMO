using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DaoCompetidor
    {
        static async Task<bool> InsertarCompetidor(Competidor competidor)
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
        static async Task<Competidor> ObtenerCompetidorPorUsuarioIdAsync(int idUsuario)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "SELECT * FROM competidor WHERE ID_USUARIO = @usuario";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", idUsuario);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Competidor competidor = new Competidor
                            {
                                Id_Persona = int.Parse(reader["ID_PERSONA"].ToString()),
                                Id_Usuario = int.Parse(reader["ID_USUARIO"].ToString())
                            };
                            return competidor;
                        }
                    }
                }
            }
            return null;
        }

        static async Task<Competidor> ObtenerCompetidorPorPersonaIdAsync(int idPersona)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "SELECT * FROM competidor WHERE ID_PERSONA = @persona";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@persona", idPersona);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Competidor competidor = new Competidor
                            {
                                Id_Persona = int.Parse(reader["ID_PERSONA"].ToString()),
                                Id_Usuario = int.Parse(reader["ID_USUARIO"].ToString())
                            };
                            return competidor;
                        }
                    }
                }
            }
            return null;
        }
    }
}
