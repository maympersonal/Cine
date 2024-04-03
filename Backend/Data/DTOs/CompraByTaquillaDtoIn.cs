namespace Backend.Data.DTOs;

public class CompraByUserTaquillaDtoIn
{
    public int IdP { get; set; }

    public int IdS { get; set; }

    public DateTime Fecha { get; set; }
    
    public string CiTaquillero {get;set;} = null!;

    public string Ci { get; set; } = null!;

    public decimal? Cantidad { get; set; }

    public string Correo { get; set; } = null!;

    public DateTime FechaDeCompra { get; set; }

    public ICollection<int> IdB { get; set; }=new List<int>();

    public ICollection<int> IdD { get; set; }=new List<int>();
}