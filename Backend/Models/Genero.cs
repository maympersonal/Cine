using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Genero
{
    public int IdG { get; set; }

    public string? NombreG { get; set; }

    public virtual ICollection<Pelicula> IdPs { get; set; } = new List<Pelicula>();
}
