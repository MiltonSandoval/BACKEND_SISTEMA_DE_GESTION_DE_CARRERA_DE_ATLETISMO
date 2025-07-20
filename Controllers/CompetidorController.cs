using Microsoft.AspNetCore.Mvc;

namespace BACKEND_SISTEMA_DE_GESTION_DE_CARRERA_DE_ATLETISMO.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CompetidorController : ControllerBase
    {
        [HttpGet("ListarTodos")]
        public IActionResult Get()
        {
            
            return Ok("Lista de competidores");
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Aquí podrías buscar el competidor por ID en una base de datos o lista
            return Ok($"Detalles del competidor con ID: {id}");
        }
        [HttpPost("Registrar")]
        public IActionResult Post([FromBody] string competidor)
        {
            // Aquí podrías agregar el competidor a una base de datos o lista
            return CreatedAtAction(nameof(Get), new { id = 1 }, competidor);
        }
        [HttpPut("Actualizar/{id}")]

    }
}
