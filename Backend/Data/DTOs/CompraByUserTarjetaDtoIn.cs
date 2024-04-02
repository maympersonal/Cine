namespace Backend.Data.DTOs;

public class CompraByUserTarjetaDtoIn
{
    public int IdP { get; set; }

    public int IdS { get; set; }

    public DateTime Fecha { get; set; }

    public string Ci { get; set; } = null!;

    public decimal? Cantidad { get; set; }

    public string CodigoT { get; set; } = null!;

    public DateTime FechaDeCompra { get; set; }

    public ICollection<int> IdB { get; set; }=new List<int>();

    public ICollection<int> IdD { get; set; }=new List<int>();
}