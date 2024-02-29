using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Pago
{
    public int IdPg { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Efectivo? Efectivo { get; set; }

    public virtual Punto? Punto { get; set; }

    public virtual Web? Web { get; set; }
}
