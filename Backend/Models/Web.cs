using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Web")]
public partial class Web
{
    [Column("IdPg")]
    public int IdPg { get; set; }

    [Column("CodigoT", TypeName = "varchar(18)")]
    public string? CodigoT { get; set; }

    [Column("Cantidad", TypeName = "decimal(10, 2)")]
    public decimal? Cantidad { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(CodigoT))]
    public virtual Tarjetum? CodigoTNavigation { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(IdPg))]
    public virtual Pago IdPgNavigation { get; set; } = null!;
}