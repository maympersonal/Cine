using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Sesion
{
    public int IdP { get; set; }

    public int IdS { get; set; }

    public DateTime Fecha { get; set; }

    [JsonIgnore]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
    [JsonIgnore]
    public virtual Pelicula IdPNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Sala IdSNavigation { get; set; } = null!;
}
