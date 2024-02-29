using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Sala
{
    public int IdS { get; set; }

    public int? Capacidad { get; set; }

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}
