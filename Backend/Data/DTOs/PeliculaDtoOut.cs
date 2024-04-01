namespace Backend.Data.DTOs;


public class PeliculaDtoOut
{
    public int? IdP {get;set;}
    public string? Sinopsis { get; set; }

    public int? Anno { get; set; }

    public int? Nacionalidad { get; set; }

    public int? Duración { get; set; }

    public string? Titulo { get; set; }

    public string? Imagen { get; set; }

    public string? Trailer { get; set; }

    public virtual List<int> IdAs { get; set; } = new List<int>();

    public virtual List<int> IdGs { get; set; } = new List<int>();
}
