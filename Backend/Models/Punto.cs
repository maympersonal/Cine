using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Puntos")]
public partial class Punto
{
    [Key]
    [Column("IdPg")]
    public int IdPg { get; set; }

    [Column("Gastados")]
    public int? Gastados { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(Pago.IdPg))]
    public virtual Pago IdPgNavigation { get; set; } = null!;
}
