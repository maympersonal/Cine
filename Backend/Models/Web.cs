using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Web
{
    public int IdPg { get; set; }

    public string? CodigoT { get; set; }

    public decimal? Cantidad { get; set; }

    public virtual Tarjetum? CodigoTNavigation { get; set; }

    public virtual Pago IdPgNavigation { get; set; } = null!;
}
