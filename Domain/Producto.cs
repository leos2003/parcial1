using System;
using System.Collections.Generic;

namespace parcial1.Domain;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Categoria { get; set; }

    public decimal Precio { get; set; }

    public int? Stock { get; set; }

    public DateTime? FechaIngreso { get; set; }
}
