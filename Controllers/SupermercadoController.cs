using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using parcial1.Domain;

[ApiController]
[Route("api/[controller]")]
public class SupermercadoController : ControllerBase
{
    private readonly SupermercadoDbContext _context;

    public SupermercadoController(SupermercadoDbContext context)
    {
        _context = context;
    }

    // POST: api/Supermercado/productos
    [HttpPost("productos")]
    public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
    {
        if (producto == null)
            return BadRequest("Producto inválido");

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CrearProducto), new { id = producto.ProductoId }, producto);
    }

    // GET: api/Supermercado/consumidores
    [HttpGet("consumidores")]
    public async Task<IActionResult> GetConsumidores()
    {
        var consumidores = await _context.Consumidores.ToListAsync();
        return Ok(consumidores);
    }
}
