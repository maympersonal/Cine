using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Punto
{
    public int IdPg { get; set; }

    public int? Gastados { get; set; }

    public virtual Pago IdPgNavigation { get; set; } = null!;
}
