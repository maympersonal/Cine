using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Pelicula
{
    public int IdP { get; set; }

    public string? Sinopsis { get; set; }

    public int? Anno { get; set; }

    public int? Nacionalidad { get; set; }

    public int? Duración { get; set; }

    public string? Titulo { get; set; }

    public string? Imagen { get; set; }

    public string? Trailer { get; set; }

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();

    public virtual ICollection<Actor> IdAs { get; set; } = new List<Actor>();

    public virtual ICollection<Genero> IdGs { get; set; } = new List<Genero>();
}
