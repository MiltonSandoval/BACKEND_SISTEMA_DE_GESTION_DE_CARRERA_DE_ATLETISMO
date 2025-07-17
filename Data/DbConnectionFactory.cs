using System.Data.Common;
using System.Data.SqlClient;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data
{
    public class DbConnectionFactory
    {
        private readonly string _CadenaConexion;

        public DbConnectionFactory(IConfiguration configuracion)
        {
            _CadenaConexion = configuracion.GetConnectionString("DefaultConnection");
        }

        public DbConnection CrearConexion()
        {
            if (string.IsNullOrEmpty(_CadenaConexion))
            {
                throw new InvalidOperationException("La cadena de conexión no está configurada.");
            }

            var conexion = new SqlConnection(_CadenaConexion);
            conexion.Open();
            return conexion;
        }
    }
}
