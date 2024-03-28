namespace Backend.Data.DTOs;

public class CompraDtoIn
{
    public int IdP { get; set; }

    public int IdS { get; set; }

    public DateTime Fecha { get; set; }

    public string Ci { get; set; } = null!;

    public int IdPg { get; set; }

    public string? Tipo { get; set; }

    public DateTime FechaDeCompra { get; set; }
}