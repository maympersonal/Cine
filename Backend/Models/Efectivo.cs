using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Efectivo")]
public partial class Efectivo
{
    [Key]
    [Column("IdPg")]
    public int IdPg { get; set; }

    [Column("CantidadE",TypeName="Decimal(10,2)")]
    public decimal? CantidadE { get; set; }

    [JsonIgnore]
    [ForeignKey("IdPg")]
    public virtual Pago IdPgNavigation { get; set; } = null!;
}