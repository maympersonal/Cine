namespace Backend.Data.DTOs;

public class ClienteDtoIn
{
    public string Ci { get; set; } = null!;

    public string? Correo { get; set; }

    public bool? Confiabilidad { get; set; }
}