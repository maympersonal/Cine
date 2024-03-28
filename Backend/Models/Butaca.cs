using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Butaca
{
    public int IdB { get; set; }

    public int IdS { get; set; }

    [JsonIgnore]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
