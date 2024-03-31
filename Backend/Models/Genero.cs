using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Genero
{
    public int IdG { get; set; }

    public string? NombreG { get; set; }

    [JsonIgnore]
    public virtual ICollection<Pelicula> IdPs { get; set; } = new List<Pelicula>();
}
