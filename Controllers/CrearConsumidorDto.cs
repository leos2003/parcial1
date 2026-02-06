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

    // GET: api/Consumidores
    [HttpGet]
    public async Task<IActionResult> GetConsumidores()
    {
        var consumidores = await _context.Consumidores.ToListAsync();
        return Ok(consumidores);
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

        return CreatedAtAction(nameof(GetConsumidores),
            new { id = consumidor.ConsumidorId },
            consumidor);
    }
}
