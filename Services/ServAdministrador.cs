using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Modelo;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DTO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.DAO;
using BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Data;
namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Servicio
{
    public class ServAdministrador
    {
        private readonly DaoAdministrador _daoAdministrador;
        public ServAdministrador(DaoAdministrador daoAdministrador)
        {
            _daoAdministrador = daoAdministrador;
        }
        public async Task<DtoPerfilAdministrador> CrearAdministrador(DtoRegistroAdministrador dto)
        {
            try
            {
                // Verificar que el email no exista
                var idUsuarioExistente = await DaoUsuario.ObtenerIdUsuarioPorEmail(dto.Email);
                if (idUsuarioExistente != -1)
                {
                    throw new Exception("El email ya está registrado en el sistema");
                }
                // 1. Crear persona (igual que competidor)
                var persona = new Persona
                {
                    Nombre = dto.Nombre,
                    Apellidos = dto.Apellido,
                    Fecha_Nacimiento = dto.FechaNacimiento,
                    Documento_Identidad = dto.DocumentoIdentidad,
                    Telefono = dto.Telefono,
                    Genero = dto.Genero,
                    Nacionalidad = dto.Nacionalidad
                };
                bool personaCreada = await DaoPersona.CrearPersonaAsync(persona);
                if (!personaCreada)
                {
                    throw new Exception("Error al crear persona");
                }
                // 2. Obtener ID de persona
                int idPersona = await DaoPersona.ObtenerIdPersonaPorDocumentoAsyn(dto.DocumentoIdentidad);
                if (idPersona == -1)
                {
                    throw new Exception("Error al obtener ID de persona");
                }
                // 3. Crear usuario con rol 3 (administrador)
                var usuario = new Usuario
                {
                    Email = dto.Email,
                    Password = dto.Password,
                    Estado = true,
                    Id_Rol = 3 // Rol de administrador
                };
                bool usuarioCreado = await DaoUsuario.CrearUsuarioAsync(usuario);
                if (!usuarioCreado)
                {
                    throw new Exception("Error al crear usuario");
                }
                // 4. Obtener ID de usuario
                int idUsuario = await DaoUsuario.ObtenerIdUsuarioPorEmail(dto.Email);
                if (idUsuario == -1)
                {
                    throw new Exception("Error al obtener ID de usuario");
                }
                // 5. Crear administrador
                var administrador = new Administrador
                {
                    Id_Usuario = idUsuario,
                    Id_Persona = idPersona
                };
                var administradorCreado = await DaoAdministrador.Crear(administrador);
                // 6. Retornar perfil básico
                return new DtoPerfilAdministrador
                {
                    IdAdministrador = administradorCreado.Id_Administrador,
                    Nombre = persona.Nombre,
                    Apellidos = persona.Apellidos,
                    FechaNacimiento = persona.Fecha_Nacimiento,
                    DocumentoIdentidad = persona.Documento_Identidad,
                    Telefono = persona.Telefono,
                    Genero = persona.Genero,
                    Nacionalidad = persona.Nacionalidad,
                    Email = usuario.Email,
                    FechaRegistro = DateTime.Now,
                    Estado = usuario.Estado
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear administrador: {ex.Message}");
            }
        }
        // Implementaciones básicas para otros métodos
        public async Task<DtoPerfilAdministrador?> LoginAdministrador(string email, string password)
        {
            // Implementación básica por ahora
            return null;
        }
        public async Task<List<DtoPerfilAdministrador>> ListarAdministradores()
        {
            // Implementación básica por ahora
            return new List<DtoPerfilAdministrador>();
        }
        public async Task<bool> CambiarEstadoAdministrador(int idAdministrador)
        {
            // Implementación básica por ahora
            return false;
        }
        public async Task<DtoEstadisticasAdministrador> ObtenerEstadisticas()
        {
            // Implementación básica por ahora
            return new DtoEstadisticasAdministrador
            {
                TotalUsuarios = 0,
                TotalCompetidores = 0,
                TotalOrganizadores = 0,
                TotalCarreras = 0,
                TotalClubes = 0,
                IngresosTotales = 0
            };
        }
        public async Task<DtoPerfilAdministrador?> ObtenerPerfilAdministrador(int idAdministrador)
        {
            // Implementación básica por ahora
            return null;
        }
        public async Task<DtoPerfilAdministrador> ActualizarPerfilAdministrador(DtoPerfilAdministrador dto)
        {
            // Implementación básica por ahora
            return dto;
        }
    }
}