using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Sesion
{
    public int IdP { get; set; }

    public int IdS { get; set; }

    public DateTime Fecha { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Pelicula IdPNavigation { get; set; } = null!;

    public virtual Sala IdSNavigation { get; set; } = null!;
}
