using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Compra
{
    public int IdP { get; set; }

    public int IdS { get; set; }

    public DateTime Fecha { get; set; }

    public string Ci { get; set; } = null!;

    public int IdPg { get; set; }

    public virtual Cliente CiNavigation { get; set; } = null!;

    public virtual Pago IdPgNavigation { get; set; } = null!;

    public virtual Sesion Sesion { get; set; } = null!;

    public virtual ICollection<Butaca> IdBs { get; set; } = new List<Butaca>();

    public virtual ICollection<Descuento> IdDs { get; set; } = new List<Descuento>();
}
