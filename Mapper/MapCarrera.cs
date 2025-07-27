using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Mapper
{
    public class MapCarrera
    {
        public static Carrera ObtenerCarreraDelDto(DtoCrearCarrera dtoCrearCarrera)
        {
            Carrera carrera = new Carrera();
            carrera.Id_Organizador = dtoCrearCarrera.Id_Organizador;
            carrera.Id_Categoria = dtoCrearCarrera.Id_Categoria;
            carrera.Titulo_Carrera = dtoCrearCarrera.Titulo_Carrera;
            carrera.Fecha_Inicio_Inscripcion = dtoCrearCarrera.Fecha_Inicio_Inscripcion;
            carrera.Fecha_Fin_Inscripcion = dtoCrearCarrera.Fecha_Fin_Inscripcion;
            carrera.Lugar = dtoCrearCarrera.Lugar;
            carrera.Cantidad_Maxima_Participante = dtoCrearCarrera.Cantidad_Maxima_Participante;
            carrera.Precio_Inscripcion = dtoCrearCarrera.Precio_Inscripcion;
            return carrera;
        }

        public static Carrera ObtenerCarreraDelDtoCompleto(DtoCarrera dtoCarrera)
        {
            Carrera carrera = new Carrera();
            carrera.Id_Carrera = dtoCarrera.Id_Carrera;
            carrera.Id_Organizador = dtoCarrera.Id_Organizador;
            carrera.Id_Categoria = dtoCarrera.Id_Categoria;
            carrera.Titulo_Carrera = dtoCarrera.Titulo_Carrera;
            carrera.Fecha_Inicio_Inscripcion = dtoCarrera.Fecha_Inicio_Inscripcion;
            carrera.Fecha_Fin_Inscripcion = dtoCarrera.Fecha_Fin_Inscripcion;
            carrera.Lugar = dtoCarrera.Lugar;
            carrera.Cantidad_Maxima_Participante = dtoCarrera.Cantidad_Maxima_Participante;
            carrera.Estado_Carrera = dtoCarrera.Estado_Carrera;
            carrera.Precio_Inscripcion = dtoCarrera.Precio_Inscripcion;
            return carrera;
        }
        public static DtoCarrera ObtenerDtoDeCarrera(Carrera carrera)
        {
            DtoCarrera dtoCarrera = new DtoCarrera();
            dtoCarrera.Id_Carrera = carrera.Id_Carrera;
            dtoCarrera.Id_Organizador = carrera.Id_Organizador;
            dtoCarrera.Id_Categoria = carrera.Id_Categoria;
            dtoCarrera.Titulo_Carrera = carrera.Titulo_Carrera;
            dtoCarrera.Fecha_Inicio_Inscripcion = carrera.Fecha_Inicio_Inscripcion;
            dtoCarrera.Fecha_Fin_Inscripcion = carrera.Fecha_Fin_Inscripcion;
            dtoCarrera.Lugar = carrera.Lugar;
            dtoCarrera.Cantidad_Maxima_Participante = carrera.Cantidad_Maxima_Participante;
            dtoCarrera.Estado_Carrera = carrera.Estado_Carrera;
            dtoCarrera.Precio_Inscripcion = carrera.Precio_Inscripcion;

            // AGREGAR ESTAS LÍNEAS
            dtoCarrera.Nombre_Categoria = carrera.Nombre_Categoria;
            dtoCarrera.Nombre_Organizador = carrera.Nombre_Organizador;

            return dtoCarrera;
        }
    }
}