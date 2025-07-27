using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Mapper
{
    public class MapResultado
    {
        public static ResultadoCarrera ObtenerResultadoDelDto(DtoResultadoCarrera dtoResultado)
        {
            ResultadoCarrera resultado = new ResultadoCarrera();
            resultado.Id_Carrera = dtoResultado.Id_Carrera;
            resultado.Id_Competidor = dtoResultado.Id_Competidor;
            resultado.Id_Club = dtoResultado.Id_Club;
            resultado.Puntuacion = dtoResultado.Puntuacion;

            // CONVERTIR STRING A TIMESPAN
            if (TimeSpan.TryParse(dtoResultado.Tiempo, out TimeSpan tiempo))
            {
                resultado.Tiempo = tiempo;
            }
            else
            {
                // Valor por defecto si no se puede parsear
                resultado.Tiempo = TimeSpan.Zero;
            }

            resultado.Posicion = dtoResultado.Posicion;
            return resultado;
        }

        public static DtoResultadoCarrera ObtenerDtoDeResultado(ResultadoCarrera resultado)
        {
            DtoResultadoCarrera dtoResultado = new DtoResultadoCarrera();
            dtoResultado.Id_Carrera = resultado.Id_Carrera;
            dtoResultado.Id_Competidor = resultado.Id_Competidor;
            dtoResultado.Id_Club = resultado.Id_Club;
            dtoResultado.Puntuacion = resultado.Puntuacion;

            // CONVERTIR TIMESPAN A STRING
            dtoResultado.Tiempo = resultado.Tiempo.ToString(@"hh\:mm\:ss");

            dtoResultado.Posicion = resultado.Posicion;
            return dtoResultado;
        }
    }
}