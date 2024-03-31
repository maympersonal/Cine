using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Actor")]
public partial class Actor
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("idA")]
    public int IdA { get; set; }

    [Required]
    [StringLength(100)]
    [Column("NombreA")]
    public string? NombreA { get; set; }


    [JsonIgnore]
    ////[InverseProperty(nameof(Pelicula.IdAs))]
    //[NotMapped]
    public virtual ICollection<Pelicula> IdPs { get; set; } = new List<Pelicula>();
}
