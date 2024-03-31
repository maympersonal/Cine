using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Genero")]
public partial class Genero
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
    [Column("IdG")]
    public int IdG { get; set; }

    [Column("NombreG",TypeName = "varchar(30)")]
    public string? NombreG { get; set; }
    
    [JsonIgnore]
    [InverseProperty(nameof(Pelicula.IdGs))]
    public virtual ICollection<Pelicula> IdPs { get; set; } = new List<Pelicula>();
}