namespace Backend.Data.DTOs;

public class UsuarioDtoIn
{
    public string Ci { get; set; } = null!;

    public string? NombreS { get; set; }

    public string? Apellidos { get; set; }

    public string? Correo { get; set; }

    public string Contrasena { get; set; } =null!;
}