using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DaoCategoria
    {
        public static async Task<int> CrearCategoria(Categoria categoria)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"INSERT INTO CATEGORIA 
                     (NOMBRE_CATEGORIA, EDAD_MINIMA, EDAD_MAXIMA, GENERO, DISTANCIA) 
                     VALUES (@nombre, @edad_min, @edad_max, @genero, @distancia)";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", categoria.Nombre_Categoria);
                    cmd.Parameters.AddWithValue("@edad_min", categoria.Edad_Minima);
                    cmd.Parameters.AddWithValue("@edad_max", categoria.Edad_Maxima);
                    cmd.Parameters.AddWithValue("@genero", categoria.Genero);
                    cmd.Parameters.AddWithValue("@distancia", categoria.Distancia);

                    await cmd.ExecuteNonQueryAsync();
                    return (int)cmd.LastInsertedId;
                }
            }
        }

        public static async Task<Categoria> ObtenerCategoriaPorId(int id)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT * FROM CATEGORIA WHERE ID_CATEGORIA = @id";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.ExecuteNonQueryAsync();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Categoria
                            {
                                Id_Categoria = int.Parse(reader["ID_CATEGORIA"].ToString()),
                                Nombre_Categoria = reader["NOMBRE_CATEGORIA"].ToString(),
                                Edad_Minima = int.Parse(reader["EDAD_MINIMA"].ToString()),
                                Edad_Maxima = int.Parse(reader["EDAD_MAXIMA"].ToString()),
                                Genero = reader["GENERO"].ToString(),
                                Distancia = int.Parse(reader["DISTANCIA"].ToString())
                            };
                        }
                    }
                }
            }
            return null;
        }

        public static async Task<bool> ActualizarCategoria(Categoria categoria)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"UPDATE CATEGORIA SET 
                    NOMBRE_CATEGORIA = @nombre, 
                    EDAD_MINIMA = @edad_min, 
                    EDAD_MAXIMA = @edad_max, 
                    GENERO = @genero, 
                    DISTANCIA = @distancia 
                    WHERE ID_CATEGORIA = @id";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", categoria.Id_Categoria);
                    cmd.Parameters.AddWithValue("@nombre", categoria.Nombre_Categoria);
                    cmd.Parameters.AddWithValue("@edad_min", categoria.Edad_Minima);
                    cmd.Parameters.AddWithValue("@edad_max", categoria.Edad_Maxima);
                    cmd.Parameters.AddWithValue("@genero", categoria.Genero);
                    cmd.Parameters.AddWithValue("@distancia", categoria.Distancia);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }
    }
}