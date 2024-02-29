using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Descuento
{
    public int IdD { get; set; }

    public string? NombreD { get; set; }

    public double? Porciento { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
