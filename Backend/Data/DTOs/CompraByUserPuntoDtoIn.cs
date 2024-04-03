namespace Backend.Data.DTOs;

public class CompraByUserPuntoDtoIn
{
    public int IdP { get; set; }

    public int IdS { get; set; }

    public DateTime Fecha { get; set; }

    public string Ci { get; set; } = null!;

    public int? Cantidad { get; set; }

    public DateTime FechaDeCompra { get; set; }

    public ICollection<int> IdB { get; set; }=new List<int>();
}