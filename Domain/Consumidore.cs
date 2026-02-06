using System;
using System.Collections.Generic;

namespace parcial1.Domain;

public partial class Consumidore
{
    public int ConsumidorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public DateOnly? FechaRegistro { get; set; }
}
