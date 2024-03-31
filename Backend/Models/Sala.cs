using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Sala")]
public partial class Sala
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("IdS")]
    public int IdS { get; set; }

    [Column("Capacidad")]
    public int? Capacidad { get; set; }

    [JsonIgnore]
    [InverseProperty(nameof(Sesion.IdSNavigation))]
    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}
