using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Actor
{
    public int IdA { get; set; }

    public string? NombreA { get; set; }

    public virtual ICollection<Pelicula> IdPs { get; set; } = new List<Pelicula>();
}
