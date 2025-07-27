using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DaoCarrera
    {
        public static async Task<Carrera> CrearCarrera(Carrera carrera)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"INSERT INTO CARRERA 
                     (ID_ORGANIZADOR, ID_CATEGORIA, TITULO_CARRERA, FECHA_INICIO_INSCRIPCION, 
                      FECHA_FIN_INSCRIPCION, LUGAR, CANTIDAD_MAXIMA_PARTICIPANTE, ESTADO_CARRERA, PRECIO_INSCRIPCION) 
                     VALUES (@organizador, @categoria, @titulo, @fecha_inicio, @fecha_fin, 
                             @lugar, @cantidad_max, @estado, @precio)";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@organizador", carrera.Id_Organizador);
                    cmd.Parameters.AddWithValue("@categoria", carrera.Id_Categoria);
                    cmd.Parameters.AddWithValue("@titulo", carrera.Titulo_Carrera);
                    cmd.Parameters.AddWithValue("@fecha_inicio", carrera.Fecha_Inicio_Inscripcion);
                    cmd.Parameters.AddWithValue("@fecha_fin", carrera.Fecha_Fin_Inscripcion);
                    cmd.Parameters.AddWithValue("@lugar", carrera.Lugar);
                    cmd.Parameters.AddWithValue("@cantidad_max", carrera.Cantidad_Maxima_Participante);
                    cmd.Parameters.AddWithValue("@estado", carrera.Estado_Carrera);
                    cmd.Parameters.AddWithValue("@precio", carrera.Precio_Inscripcion);

                    await cmd.ExecuteNonQueryAsync();

                    long idGenerado = cmd.LastInsertedId;

                    return new Carrera
                    {
                        Id_Carrera = (int)idGenerado,
                        Id_Organizador = carrera.Id_Organizador,
                        Id_Categoria = carrera.Id_Categoria,
                        Titulo_Carrera = carrera.Titulo_Carrera,
                        Fecha_Inicio_Inscripcion = carrera.Fecha_Inicio_Inscripcion,
                        Fecha_Fin_Inscripcion = carrera.Fecha_Fin_Inscripcion,
                        Lugar = carrera.Lugar,
                        Cantidad_Maxima_Participante = carrera.Cantidad_Maxima_Participante,
                        Estado_Carrera = carrera.Estado_Carrera,
                        Precio_Inscripcion = carrera.Precio_Inscripcion
                    };
                }
            }
        }

        public static async Task<Carrera> ObtenerCarreraPorId(int id)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT 
                    c.ID_CARRERA,
                    c.ID_ORGANIZADOR,
                    c.ID_CATEGORIA,
                    c.TITULO_CARRERA,
                    c.FECHA_INICIO_INSCRIPCION,
                    c.FECHA_FIN_INSCRIPCION,
                    c.LUGAR,
                    c.CANTIDAD_MAXIMA_PARTICIPANTE,
                    c.ESTADO_CARRERA,
                    c.PRECIO_INSCRIPCION,
                    cat.NOMBRE_CATEGORIA,
                    CONCAT(p.NOMBRE, ' ', p.APELLIDOS) as NOMBRE_ORGANIZADOR
                    FROM CARRERA c
                    JOIN CATEGORIA cat ON c.ID_CATEGORIA = cat.ID_CATEGORIA
                    JOIN ORGANIZADOR o ON c.ID_ORGANIZADOR = o.ID_ORGANIZADOR
                    JOIN PERSONA p ON o.ID_PERSONA = p.ID_PERSONA
                    WHERE c.ID_CARRERA = @idcarrera";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idcarrera", id);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Carrera carrera = new Carrera
                            {
                                Id_Carrera = int.Parse(reader["ID_CARRERA"].ToString()),
                                Id_Organizador = int.Parse(reader["ID_ORGANIZADOR"].ToString()),
                                Id_Categoria = int.Parse(reader["ID_CATEGORIA"].ToString()),
                                Titulo_Carrera = reader["TITULO_CARRERA"].ToString(),
                                Fecha_Inicio_Inscripcion = DateTime.Parse(reader["FECHA_INICIO_INSCRIPCION"].ToString()),
                                Fecha_Fin_Inscripcion = DateTime.Parse(reader["FECHA_FIN_INSCRIPCION"].ToString()),
                                Lugar = reader["LUGAR"].ToString(),
                                Cantidad_Maxima_Participante = int.Parse(reader["CANTIDAD_MAXIMA_PARTICIPANTE"].ToString()),
                                Estado_Carrera = bool.Parse(reader["ESTADO_CARRERA"].ToString()),
                                Precio_Inscripcion = decimal.Parse(reader["PRECIO_INSCRIPCION"].ToString()),
                                Nombre_Categoria = reader["NOMBRE_CATEGORIA"].ToString(),
                                Nombre_Organizador = reader["NOMBRE_ORGANIZADOR"].ToString()
                            };
                            return carrera;
                        }
                    }
                }
            }
            return null;
        }

        public static async Task<List<Carrera>> ObtenerCarrerasPorOrganizador(int idOrganizador)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            List<Carrera> carreras = new List<Carrera>();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT 
                    c.ID_CARRERA,
                    c.ID_ORGANIZADOR,
                    c.ID_CATEGORIA,
                    c.TITULO_CARRERA,
                    c.FECHA_INICIO_INSCRIPCION,
                    c.FECHA_FIN_INSCRIPCION,
                    c.LUGAR,
                    c.CANTIDAD_MAXIMA_PARTICIPANTE,
                    c.ESTADO_CARRERA,
                    c.PRECIO_INSCRIPCION,
                    cat.NOMBRE_CATEGORIA,
                    CONCAT(p.NOMBRE, ' ', p.APELLIDOS) as NOMBRE_ORGANIZADOR
                    FROM CARRERA c
                    JOIN CATEGORIA cat ON c.ID_CATEGORIA = cat.ID_CATEGORIA
                    JOIN ORGANIZADOR o ON c.ID_ORGANIZADOR = o.ID_ORGANIZADOR
                    JOIN PERSONA p ON o.ID_PERSONA = p.ID_PERSONA
                    WHERE c.ID_ORGANIZADOR = @idorganizador";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idorganizador", idOrganizador);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Carrera carrera = new Carrera
                            {
                                Id_Carrera = int.Parse(reader["ID_CARRERA"].ToString()),
                                Id_Organizador = int.Parse(reader["ID_ORGANIZADOR"].ToString()),
                                Id_Categoria = int.Parse(reader["ID_CATEGORIA"].ToString()),
                                Titulo_Carrera = reader["TITULO_CARRERA"].ToString(),
                                Fecha_Inicio_Inscripcion = DateTime.Parse(reader["FECHA_INICIO_INSCRIPCION"].ToString()),
                                Fecha_Fin_Inscripcion = DateTime.Parse(reader["FECHA_FIN_INSCRIPCION"].ToString()),
                                Lugar = reader["LUGAR"].ToString(),
                                Cantidad_Maxima_Participante = int.Parse(reader["CANTIDAD_MAXIMA_PARTICIPANTE"].ToString()),
                                Estado_Carrera = bool.Parse(reader["ESTADO_CARRERA"].ToString()),
                                Precio_Inscripcion = decimal.Parse(reader["PRECIO_INSCRIPCION"].ToString()),
                                Nombre_Categoria = reader["NOMBRE_CATEGORIA"].ToString(),
                                Nombre_Organizador = reader["NOMBRE_ORGANIZADOR"].ToString()
                            };
                            carreras.Add(carrera);
                        }
                    }
                }
            }
            return carreras;
        }

        public static async Task<bool> ActualizarCarrera(Carrera carrera)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"UPDATE CARRERA SET 
                    ID_CATEGORIA = @categoria, 
                    TITULO_CARRERA = @titulo, 
                    FECHA_INICIO_INSCRIPCION = @fecha_inicio, 
                    FECHA_FIN_INSCRIPCION = @fecha_fin, 
                    LUGAR = @lugar, 
                    CANTIDAD_MAXIMA_PARTICIPANTE = @cantidad_max, 
                    ESTADO_CARRERA = @estado, 
                    PRECIO_INSCRIPCION = @precio 
                    WHERE ID_CARRERA = @id_carrera";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_carrera", carrera.Id_Carrera);
                    cmd.Parameters.AddWithValue("@categoria", carrera.Id_Categoria);
                    cmd.Parameters.AddWithValue("@titulo", carrera.Titulo_Carrera);
                    cmd.Parameters.AddWithValue("@fecha_inicio", carrera.Fecha_Inicio_Inscripcion);
                    cmd.Parameters.AddWithValue("@fecha_fin", carrera.Fecha_Fin_Inscripcion);
                    cmd.Parameters.AddWithValue("@lugar", carrera.Lugar);
                    cmd.Parameters.AddWithValue("@cantidad_max", carrera.Cantidad_Maxima_Participante);
                    cmd.Parameters.AddWithValue("@estado", carrera.Estado_Carrera);
                    cmd.Parameters.AddWithValue("@precio", carrera.Precio_Inscripcion);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }

        public static async Task<bool> CambiarEstadoCarrera(int idCarrera)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"UPDATE CARRERA SET ESTADO_CARRERA = NOT ESTADO_CARRERA WHERE ID_CARRERA = @id_carrera";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_carrera", idCarrera);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }

        public static async Task<List<Categoria>> ListarCategorias()
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            List<Categoria> categorias = new List<Categoria>();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = @"SELECT * FROM CATEGORIA";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria categoria = new Categoria
                            {
                                Id_Categoria = int.Parse(reader["ID_CATEGORIA"].ToString()),
                                Nombre_Categoria = reader["NOMBRE_CATEGORIA"].ToString(),
                                Edad_Minima = int.Parse(reader["EDAD_MINIMA"].ToString()),
                                Edad_Maxima = int.Parse(reader["EDAD_MAXIMA"].ToString()),
                                Genero = reader["GENERO"].ToString(),
                                Distancia = int.Parse(reader["DISTANCIA"].ToString())
                            };
                            categorias.Add(categoria);
                        }
                    }
                }
            }
            return categorias;
        }
    }
}