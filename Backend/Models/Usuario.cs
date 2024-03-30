using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Usuario
{
    public string Ci { get; set; } = null!;

    public string? NombreS { get; set; }

    public string? Apellidos { get; set; }

    public int? Puntos { get; set; }

    [JsonIgnore]
    public string? Codigo { get; set; }

    [JsonIgnore]
    public byte[]? Contrasena { get; set; }

    public string? Rol { get; set; }

    [JsonIgnore]
    public virtual Cliente CiNavigation { get; set; } = null!;
}
