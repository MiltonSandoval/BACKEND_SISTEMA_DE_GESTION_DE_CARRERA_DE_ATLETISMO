using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Mapper;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Services
{
    public class ServOrganizador
    {
        public async Task<DtoPerfilOrganizador> RegistrarOrganizador(DtoRegistroUsuario dtoRegistroUsuario)
        {
            try
            {
                Persona persona = MapPerfilUsuario.ObtenerPersonaDelDtoRegistro(dtoRegistroUsuario);
                bool personaCreada = await DaoPersona.CrearPersonaAsync(persona);

                if (personaCreada)
                {
                    int idPersona = await DaoPersona.ObtenerIdPersonaPorDocumentoAsyn(persona.Documento_Identidad);
                    Usuario usuario = MapPerfilUsuario.ObtenerUsuarioDelDtoRegistro(dtoRegistroUsuario);
                    usuario.Estado = true;
                    usuario.Id_Rol = 2; // Rol organizador

                    bool usuarioCreado = await DaoUsuario.CrearUsuarioAsync(usuario);

                    if (usuarioCreado)
                    {
                        int idUsuario = await DaoUsuario.ObtenerIdUsuarioPorEmail(usuario.Email);
                        Organizador organizador = new Organizador
                        {
                            Id_Persona = idPersona,
                            Id_Usuario = idUsuario
                        };

                        bool organizadorCreado = await DaoOrganizador.CrearOrganizador(organizador);

                        if (organizadorCreado)
                        {
                            return await DaoOrganizador.ObtenerOrganizadorPorIdPersonaAsync(idPersona);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar organizador: {ex.Message}");
            }
            return null;
        }

        public async Task<DtoPerfilOrganizador> LoginOrganizador(string email, string password)
        {
            try
            {
                return await DaoOrganizador.ObtenerOrganizadorPorCorreoYContra(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en login: {ex.Message}");
            }
        }

        public async Task<DtoPerfilOrganizador> ActualizarPerfilOrganizador(DtoPerfilOrganizador dtoPerfilOrganizador)
        {
            try
            {
                // Convertir DtoPerfilOrganizador a entidades Persona y Usuario
                Persona persona = new Persona
                {
                    Id_Persona = await DaoOrganizador.ObtenerIdPersonaDeOrganizador(dtoPerfilOrganizador.Id_Organizador),
                    Nombre = dtoPerfilOrganizador.Nombre,
                    Apellidos = dtoPerfilOrganizador.Apellidos,
                    Fecha_Nacimiento = dtoPerfilOrganizador.Fecha_nacimiento,
                    Documento_Identidad = dtoPerfilOrganizador.Documento_Identidad,
                    Telefono = dtoPerfilOrganizador.Telefono,
                    Genero = dtoPerfilOrganizador.Genero,
                    Nacionalidad = dtoPerfilOrganizador.Nacionalidad
                };

                Usuario usuario = new Usuario
                {
                    Id_User = await DaoOrganizador.ObtenerIdUsuarioDeOrganizador(dtoPerfilOrganizador.Id_Organizador),
                    Email = dtoPerfilOrganizador.Email,
                    Password = dtoPerfilOrganizador.Password,
                    Estado = dtoPerfilOrganizador.Estado,
                    Id_Rol = 2 // Rol organizador
                };

                bool personaActualizada = await DaoPersona.ActualizarPersona(persona);
                bool usuarioActualizado = await DaoUsuario.ActualizarUsuario(usuario);

                if (personaActualizada && usuarioActualizado)
                {
                    return await DaoOrganizador.ObtenerOrganizadorCompletoPorIdAsync(dtoPerfilOrganizador.Id_Organizador);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar perfil: {ex.Message}");
            }
            return null;
        }
        // AGREGAR ESTOS MÉTODOS al servicio existente

        public async Task<DtoPerfilOrganizador> ActualizarDatosOrganizador(DtoActualizarOrganizador datos)
        {
            try
            {
                // Obtener datos actuales del organizador
                DtoPerfilOrganizador organizadorActual = await DaoOrganizador.ObtenerOrganizadorCompletoPorIdAsync(datos.Id_Organizador);

                if (organizadorActual == null)
                {
                    throw new Exception("Organizador no encontrado");
                }

                // Actualizar solo los campos permitidos
                Persona persona = new Persona
                {
                    Id_Persona = await DaoOrganizador.ObtenerIdPersonaDeOrganizador(datos.Id_Organizador),
                    Nombre = datos.Nombre,
                    Apellidos = datos.Apellidos,
                    Telefono = datos.Telefono,
                    // Mantener datos que no se cambian
                    Fecha_Nacimiento = organizadorActual.Fecha_nacimiento,
                    Documento_Identidad = organizadorActual.Documento_Identidad,
                    Genero = organizadorActual.Genero,
                    Nacionalidad = organizadorActual.Nacionalidad
                };

                Usuario usuario = new Usuario
                {
                    Id_User = await DaoOrganizador.ObtenerIdUsuarioDeOrganizador(datos.Id_Organizador),
                    Email = datos.Email,
                    // Mantener datos que no se cambian
                    Password = organizadorActual.Password,
                    Estado = organizadorActual.Estado,
                    Fecha_Registro = organizadorActual.Fecha_Registro,
                    Id_Rol = 2 // Rol organizador
                };

                bool personaActualizada = await DaoPersona.ActualizarPersona(persona);
                bool usuarioActualizado = await DaoUsuario.ActualizarUsuario(usuario);

                if (personaActualizada && usuarioActualizado)
                {
                    return await DaoOrganizador.ObtenerOrganizadorCompletoPorIdAsync(datos.Id_Organizador);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar datos: {ex.Message}");
            }
            return null;
        }

        public async Task<DtoPerfilOrganizador> ObtenerPerfilOrganizador(int idOrganizador)
        {
            try
            {
                return await DaoOrganizador.ObtenerOrganizadorCompletoPorIdAsync(idOrganizador);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener perfil: {ex.Message}");
            }
        }

        public async Task<bool> CambiarPasswordOrganizador(DtoCambiarPassword datos)
        {
            try
            {
                // Verificar contraseña actual
                DtoPerfilOrganizador organizador = await DaoOrganizador.ObtenerOrganizadorCompletoPorIdAsync(datos.Id_Organizador);

                if (organizador == null || organizador.Password != datos.PasswordActual)
                {
                    throw new Exception("Contraseña actual incorrecta");
                }

                int idUsuario = await DaoOrganizador.ObtenerIdUsuarioDeOrganizador(datos.Id_Organizador);

                // Usar el método existente en DaoUsuario para cambiar contraseña
                return await DaoUsuario.CambiarContrasenaAsyn(idUsuario, datos.PasswordNuevo, datos.PasswordActual);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cambiar contraseña: {ex.Message}");
            }
        }

        public async Task<DtoCarrera> CrearCarrera(DtoCrearCarrera dtoCrearCarrera)
        {
            try
            {
                Carrera carrera = MapCarrera.ObtenerCarreraDelDto(dtoCrearCarrera);
                carrera.Estado_Carrera = true;

                Carrera carreraCreada = await DaoCarrera.CrearCarrera(carrera);
                if (carreraCreada != null)
                {
                    return MapCarrera.ObtenerDtoDeCarrera(carreraCreada);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear carrera: {ex.Message}");
            }
            return null;
        }

        public async Task<DtoCarrera> EditarCarrera(DtoCarrera dtoCarrera)
        {
            try
            {
                Carrera carrera = MapCarrera.ObtenerCarreraDelDtoCompleto(dtoCarrera);
                bool actualizada = await DaoCarrera.ActualizarCarrera(carrera);

                if (actualizada)
                {
                    Carrera carreraActualizada = await DaoCarrera.ObtenerCarreraPorId(carrera.Id_Carrera);
                    return MapCarrera.ObtenerDtoDeCarrera(carreraActualizada);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al editar carrera: {ex.Message}");
            }
            return null;
        }

        public async Task<List<DtoCarrera>> ObtenerCarrerasDelOrganizador(int idOrganizador)
        {
            try
            {
                List<Carrera> carreras = await DaoCarrera.ObtenerCarrerasPorOrganizador(idOrganizador);
                List<DtoCarrera> dtoCarreras = new List<DtoCarrera>();

                foreach (var carrera in carreras)
                {
                    dtoCarreras.Add(MapCarrera.ObtenerDtoDeCarrera(carrera));
                }

                return dtoCarreras;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener carreras: {ex.Message}");
            }
        }
        public async Task<bool> AñadirResultadosCarrera(List<DtoResultadoCarrera> resultados)
        {
            try
            {
                foreach (var resultado in resultados)
                {
                    // LLENAR AUTOMÁTICAMENTE LOS NOMBRES
                    DtoResultadoCarrera resultadoCompleto = await CompletarDatosResultado(resultado);

                    ResultadoCarrera resultadoCarrera = MapResultado.ObtenerResultadoDelDto(resultadoCompleto);
                    bool añadido = await DaoResultadoCarrera.AñadirResultado(resultadoCarrera);
                    if (!añadido)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al añadir resultados: {ex.Message}");
            }
        }

        // NUEVO MÉTODO PARA COMPLETAR DATOS
        private async Task<DtoResultadoCarrera> CompletarDatosResultado(DtoResultadoCarrera resultado)
        {
            try
            {
                // Obtener nombre del competidor
                DtoPerfilUsuario competidor = await DaoCompetidor.ObtenerCompetidorCompletoPorIdAsync(resultado.Id_Competidor);
                string nombreCompetidor = competidor != null ? $"{competidor.Nombre} {competidor.Apellidos}" : "Competidor no encontrado";

                // Obtener nombre del club
                DtoPerfilClub club = await DaoClub.BuscarClubPorId(resultado.Id_Club);
                string nombreClub = club != null ? club.nombre : "Club no encontrado";

                // Crear resultado completo
                return new DtoResultadoCarrera
                {
                    Id_Carrera = resultado.Id_Carrera,
                    Id_Competidor = resultado.Id_Competidor,
                    Id_Club = resultado.Id_Club,
                    Puntuacion = resultado.Puntuacion,
                    Tiempo = resultado.Tiempo,
                    Posicion = resultado.Posicion,
                    Nombre_Competidor = nombreCompetidor,
                    Nombre_Club = nombreClub
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al completar datos del resultado: {ex.Message}");
            }
        }

        public async Task<bool> CambiarEstadoCarrera(int idCarrera)
        {
            try
            {
                return await DaoCarrera.CambiarEstadoCarrera(idCarrera);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cambiar estado: {ex.Message}");
            }
        }

        public async Task<List<Categoria>> ListarCategorias()
        {
            try
            {
                return await DaoCarrera.ListarCategorias();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar categorías: {ex.Message}");
            }
        }
    }
}