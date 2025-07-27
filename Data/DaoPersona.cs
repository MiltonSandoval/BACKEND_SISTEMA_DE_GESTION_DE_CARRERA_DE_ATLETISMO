using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DaoPersona
    {
        public static async Task<bool> CrearPersonaAsync(Persona persona)
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
        public static async Task<int> ObtenerIdPersonaPorDocumentoAsyn(string Documento)
        {
            DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnectionFactory.CrearConexion())
            {
                string query = "SELECT ID_PERSONA FROM PERSONA WHERE DOCUMENTO_IDENTIDAD = @documento";

                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@documento", Documento);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return int.Parse(reader["ID_PERSONA"].ToString());
                        }

                    }
                    return -1; // Retorna -1 si no se encuentra la persona
                }
            }
        }

        public static async Task<bool> EliminarPersonaAsync(int idPersona)
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

        public static async Task<bool> ActualizarPersona(Persona persona)
        {
            DbConnectionFactory dbConnFactory = new DbConnectionFactory();
            await using (MySqlConnection conexion = dbConnFactory.CrearConexion())
            {
                string query = ("UPDate Persona \r\n SET Nombre = @nombre, Apellidos = @apellidos, Telefono = @telefono, FECHA_NACIMIENTO = @fecha_nacimiento \r\nWHERE ID_PERSONA = @id_persona;");
                
                await using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_persona", persona.Id_Persona);
                    cmd.Parameters.AddWithValue("@nombre", persona.Nombre);
                    cmd.Parameters.AddWithValue("@apellidos", persona.Apellidos);
                    cmd.Parameters.AddWithValue("@telefono", persona.Telefono);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", persona.Fecha_Nacimiento);

                    return (cmd.ExecuteNonQuery() > 0);
                }
                    
            }
        }
    }
}
