using Backend.Models;

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

    public virtual ICollection<string?> IdAs { get; set; } = new List<string?>();

    public virtual ICollection<string?> IdGs { get; set; } = new List<string?>();
}
