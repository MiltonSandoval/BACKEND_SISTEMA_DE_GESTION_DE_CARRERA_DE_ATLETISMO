using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DaoPersona
    {
        public async Task<bool> CrearPersonaAsync(Persona persona)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "INSERT INTO PERSONA (NOMBRE, APELLIDOS,FECHA_NACIMIENTO, DOCUMENTO_IDENTIDAD, TELEFONO, GENERO, NACIONALIDAD) VALUES (@nombre, @apellidos,@fecha_na, @documento, @telefono, @genero, @nacionalidad)";
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", persona.Nombre);
                    cmd.Parameters.AddWithValue("@apellidos", persona.Apellidos);
                    cmd.Parameters.AddWithValue("@fecha_na", persona.Fecha_Nacimiento);
                    cmd.Parameters.AddWithValue("@documento", persona.Documento_Identidad);
                    cmd.Parameters.AddWithValue("@telefono", persona.Telefono);
                    cmd.Parameters.AddWithValue("@genero", persona.Genero);
                    cmd.Parameters.AddWithValue("@nacionalidad", persona.Nacionalidad);
                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }
        static async Task<bool> CambiarAlgoDePersona(int id_persona, Task cambio, int elemento)
        {
            List<string> Columnas = new List<string>();
            Columnas.Add("NOMBRE");
            Columnas.Add("APELLIDOS");
            Columnas.Add("FECHA_NACIMIENTO");
            Columnas.Add("TELEFONO");
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = $"UPDATE PERSONA SET {Columnas[elemento]} = @cambio WHERE ID_USER = @idUsuario";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@cambio", cambio);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }

        static async Task<Persona> ObtenerPersonaPorIdAsync(int idPersona)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "SELECT * FROM PERSONA WHERE ID_PERSONA = @persona";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@persona", idPersona);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Persona persona = new Persona();

                            persona.Id_Persona = int.Parse(reader["ID_PERSONA"].ToString());
                            persona.Nombre = reader["NOMBRE"].ToString();
                            persona.Apellidos = reader["APELLIDOS"].ToString();
                            persona.Fecha_Nacimiento = DateTime.Parse(reader["FECHA_NACIMIENTO"].ToString());
                            persona.Documento_Identidad = reader["DOCUMENTO_IDENTIDAD"].ToString();
                            persona.Telefono = reader["TELEFONO"].ToString();
                            persona.Genero = reader["GENERO"].ToString();
                            persona.Nacionalidad = reader["NACIONALIDAD"].ToString();

                            return persona;
                        }
                    }
                }
            }
            return null;
        }
        static async Task<bool> EliminarPersonaAsync(int idPersona)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "DELETE FROM PERSONA WHERE ID_PERSONA = @persona";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@persona", idPersona);
                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
        }
    }
}
