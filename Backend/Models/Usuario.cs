using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Usuario
{
    public string Ci { get; set; } = null!;

    public string? NombreS { get; set; }

    public string? Apellidos { get; set; }

    public int? Puntos { get; set; }

    public string? Codigo { get; set; }

    public byte[]? Contrasena { get; set; }

    public string? Rol { get; set; }

    public virtual Cliente CiNavigation { get; set; } = null!;
}
