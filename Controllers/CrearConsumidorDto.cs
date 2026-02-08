using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using parcial1.Domain;

[ApiController]
[Route("api/[controller]")]
public class ConsumidoresController : ControllerBase
{
    private readonly SupermercadoDbContext _context;
    private readonly ILogger<ConsumidoresController> _logger;
    private readonly IConfiguration _configuration;

    public ConsumidoresController(
        SupermercadoDbContext context,
        ILogger<ConsumidoresController> logger,
        IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetConsumidores()
    {
        _logger.LogInformation("Se solicitó la lista de consumidores");

        var consumidores = await _context.Consumidores.ToListAsync();
        return Ok(consumidores);
    }

    // GET ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetConsumidor(int id)
    {
        _logger.LogInformation("Buscando consumidor con ID {Id}", id);

        var consumidor = await _context.Consumidores.FindAsync(id);

        if (consumidor == null)
        {
            _logger.LogWarning("Consumidor con ID {Id} no encontrado", id);
            return NotFound("Consumidor no encontrado");
        }

        return Ok(consumidor);
    }

    // POST: api/Consumidores
    [HttpPost]
    public async Task<IActionResult> CrearConsumidor([FromBody] Consumidore dto)
    {
        if (dto == null)
        {
            _logger.LogWarning("Intento de crear consumidor con datos nulos");
            return BadRequest("Datos inválidos");
        }

        var consumidor = new Consumidore
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Email = dto.Email,
            Telefono = dto.Telefono,
            FechaRegistro = dto.FechaRegistro ?? DateOnly.FromDateTime(DateTime.Now)
        };

        _context.Consumidores.Add(consumidor);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Consumidor creado correctamente con ID {Id}",
            consumidor.ConsumidorId);

        return CreatedAtAction(nameof(GetConsumidor),
            new { id = consumidor.ConsumidorId },
            consumidor);
    }

    // PUT: api/Consumidores/5
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarConsumidor(int id, [FromBody] Consumidore dto)
    {
        if (dto == null)
        {
            _logger.LogWarning("Intento de actualización con datos nulos");
            return BadRequest("Datos inválidos");
        }

        var consumidor = await _context.Consumidores.FindAsync(id);

        if (consumidor == null)
        {
            _logger.LogWarning("No se encontró consumidor con ID {Id} para actualizar", id);
            return NotFound("Consumidor no encontrado");
        }

        consumidor.Nombre = dto.Nombre;
        consumidor.Apellido = dto.Apellido;
        consumidor.Email = dto.Email;
        consumidor.Telefono = dto.Telefono;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Consumidor con ID {Id} actualizado correctamente", id);

        return Ok(consumidor);
    }

    // DELETE: api/Consumidores/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarConsumidor(int id)
    {
        var consumidor = await _context.Consumidores.FindAsync(id);

        if (consumidor == null)
        {
            _logger.LogWarning("Intento de eliminar consumidor inexistente con ID {Id}", id);
            return NotFound("Consumidor no encontrado");
        }

        _context.Consumidores.Remove(consumidor);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Consumidor con ID {Id} eliminado correctamente", id);

        return NoContent(); // 204
    }
}
