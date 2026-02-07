using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using parcial1.Domain;

[ApiController]
[Route("api/[controller]")]
public class ConsumidoresController : ControllerBase
{
    private readonly SupermercadoDbContext _context;

    public ConsumidoresController(SupermercadoDbContext context)
    {
        _context = context;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetConsumidores()
    {
        var consumidores = await _context.Consumidores.ToListAsync();
        return Ok(consumidores);
    }

    // GET
    [HttpGet("{id}")]
    public async Task<IActionResult> GetConsumidor(int id)
    {
        var consumidor = await _context.Consumidores.FindAsync(id);

        if (consumidor == null)
            return NotFound("Consumidor no encontrado");

        return Ok(consumidor);
    }

    // POST: api/Consumidores
    [HttpPost]
    public async Task<IActionResult> CrearConsumidor([FromBody] Consumidore dto)
    {
        if (dto == null)
            return BadRequest("Datos inválidos");

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

        return CreatedAtAction(nameof(GetConsumidor),
            new { id = consumidor.ConsumidorId },
            consumidor);
    }

    // PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarConsumidor(int id, [FromBody] Consumidore dto)
    {
        if (dto == null)
            return BadRequest("Datos inválidos");

        var consumidor = await _context.Consumidores.FindAsync(id);

        if (consumidor == null)
            return NotFound("Consumidor no encontrado");

        consumidor.Nombre = dto.Nombre;
        consumidor.Apellido = dto.Apellido;
        consumidor.Email = dto.Email;
        consumidor.Telefono = dto.Telefono;

        await _context.SaveChangesAsync();

        return Ok(consumidor);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarConsumidor(int id)
    {
        var consumidor = await _context.Consumidores.FindAsync(id);

        if (consumidor == null)
            return NotFound("Consumidor no encontrado");

        _context.Consumidores.Remove(consumidor);
        await _context.SaveChangesAsync();

        return NoContent(); 
    }
}
