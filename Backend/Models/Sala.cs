using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Sala
{
    public int IdS { get; set; }

    public int? Capacidad { get; set; }
    [JsonIgnore]
    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}
