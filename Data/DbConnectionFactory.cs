using System.Data.Common;
using MySql.Data.MySqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DbConnectionFactory
    {
        private readonly string _CadenaConexion;

        public DbConnectionFactory( )
        {
            _CadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["DefaultConnection"];
        }

        public MySqlConnection CrearConexion()
        {
            if (string.IsNullOrEmpty(_CadenaConexion))
            {
                throw new InvalidOperationException("La cadena de conexión no está configurada.");
            }

            MySqlConnection conexion = new MySqlConnection(_CadenaConexion);
            conexion.Open();
            return conexion;
        }
    }
}
