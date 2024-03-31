using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Pelicula")]
public partial class Pelicula
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
    [Column("IdP")]
    public int IdP { get; set; }

    [Column("Sinopsis", TypeName = "text")]
    public string? Sinopsis { get; set; }

    [Column("Anno")]
    public int? Anno { get; set; }

    [Column("Nacionalidad")]
    public int? Nacionalidad { get; set; }

    [Column("Duración")]
    public int? Duración { get; set; }

    [Column("Titulo", TypeName = "varchar(50)")]
    public string? Titulo { get; set; }

    [Column("Imagen", TypeName = "text")]
    public string? Imagen { get; set; }

    [Column("Trailer", TypeName = "text")]
    public string? Trailer { get; set; }

    [JsonIgnore]
    //[InverseProperty(nameof(Sesion.IdPNavigation))]
    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();

    [JsonIgnore]
    [ForeignKey("IdA")]
    public virtual ICollection<Actor> IdAs { get; set; } = new List<Actor>();

    [JsonIgnore]
    [ForeignKey("IdG")]
    public virtual ICollection<Genero> IdGs { get; set; } = new List<Genero>();
}