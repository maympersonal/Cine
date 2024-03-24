using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Cliente
{
    public string Ci { get; set; } = null!;

    public string? Correo { get; set; }

    public bool? Confiabilidad { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<Tarjetum> Tarjeta { get; set; } = new List<Tarjetum>();

    public virtual Usuario? Usuario { get; set; }
}
