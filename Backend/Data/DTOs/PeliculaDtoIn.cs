namespace Backend.Data.DTOs;


public class PeliculaDtoIn
{
    public string? Sinopsis { get; set; }

    public int? Anno { get; set; }

    public int? Nacionalidad { get; set; }

    public int? Duración { get; set; }

    public string? Titulo { get; set; }

    public string? Imagen { get; set; }

    public string? Trailer { get; set; }

    public virtual ICollection<int> IdAs { get; set; } = new List<int>();

    public virtual ICollection<int> IdGs { get; set; } = new List<int>();
}
