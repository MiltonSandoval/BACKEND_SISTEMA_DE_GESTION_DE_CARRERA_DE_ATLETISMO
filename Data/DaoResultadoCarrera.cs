using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DaoResultadoCarrera
    {
        public static async Task<bool> AñadirResultado(ResultadoCarrera resultado)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"INSERT INTO RESULTADO_CARRERA 
                     (ID_CARRERA, ID_COMPETIDOR, ID_CLUB, PUNTUACION, TIEMPO, POSICION) 
                     VALUES (@carrera, @competidor, @club, @puntuacion, @tiempo, @posicion)";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@carrera", resultado.Id_Carrera);
                    cmd.Parameters.AddWithValue("@competidor", resultado.Id_Competidor);
                    cmd.Parameters.AddWithValue("@club", resultado.Id_Club);
                    cmd.Parameters.AddWithValue("@puntuacion", resultado.Puntuacion);
                    cmd.Parameters.AddWithValue("@tiempo", resultado.Tiempo);
                    cmd.Parameters.AddWithValue("@posicion", resultado.Posicion);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }

        public static async Task<List<ResultadoCarrera>> ObtenerResultadosPorCarrera(int idCarrera)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            List<ResultadoCarrera> resultados = new List<ResultadoCarrera>();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT * FROM RESULTADO_CARRERA WHERE ID_CARRERA = @idcarrera ORDER BY POSICION";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idcarrera", idCarrera);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResultadoCarrera resultado = new ResultadoCarrera
                            {
                                Id_Carrera = int.Parse(reader["ID_CARRERA"].ToString()),
                                Id_Competidor = int.Parse(reader["ID_COMPETIDOR"].ToString()),
                                Id_Club = int.Parse(reader["ID_CLUB"].ToString()),
                                Puntuacion = int.Parse(reader["PUNTUACION"].ToString()),
                                Tiempo = TimeSpan.Parse(reader["TIEMPO"].ToString()),
                                Posicion = int.Parse(reader["POSICION"].ToString())
                            };
                            resultados.Add(resultado);
                        }
                    }
                }
            }
            return resultados;
        }
    }
}