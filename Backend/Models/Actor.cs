using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Actor
{
    public int IdA { get; set; }

    public string? NombreA { get; set; }

    [JsonIgnore]
    public virtual ICollection<Pelicula> IdPs { get; set; } = new List<Pelicula>();
}
