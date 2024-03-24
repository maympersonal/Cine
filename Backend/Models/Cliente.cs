using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Cliente
{
    public string Ci { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Socio? Socio { get; set; }

    public virtual ICollection<Tarjetum> Tarjeta { get; set; } = new List<Tarjetum>();
}
