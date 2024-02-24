using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Efectivo
{
    public int IdPg { get; set; }

    public decimal? CantidadE { get; set; }

    public virtual Pago IdPgNavigation { get; set; } = null!;
}
