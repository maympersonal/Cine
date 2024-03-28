using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Cliente
{
    public string Ci { get; set; } = null!;

    public string? Correo { get; set; }

    public bool? Confiabilidad { get; set; }

    [JsonIgnore]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    [JsonIgnore]
    public virtual ICollection<Tarjetum> Tarjeta { get; set; } = new List<Tarjetum>();

    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; }
}
