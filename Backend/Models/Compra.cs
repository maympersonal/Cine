using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Compra")]
public partial class Compra
{
    [Key]
    [Column("IdP")]
 
    public int IdP { get; set; }

    [Key]
    [Column("IdS")]
    public int IdS { get; set; }

    [Key]
    [Column("Fecha")]
    public DateTime Fecha { get; set; }

    [Key]
    [Column("Ci", TypeName = "char(11)")]
    public string Ci { get; set; } = null!;

    [Key]
    [Column("IdPg")]
    public int IdPg { get; set; }

    [Column("Tipo", TypeName = "varchar(50)")]
    public string? Tipo { get; set; }

    [JsonIgnore]
    [Column("Eliminado")]
    public bool? Eliminado { get; set; }

    [Column("FechaDeCompra")]
    public DateTime FechaDeCompra { get; set; }

    [Column("MedioAd", TypeName = "varchar(50)")]
    public string? MedioAd { get; set; }

    [JsonIgnore]
    [ForeignKey("Ci")]
    public virtual Cliente CiNavigation { get; set; } = null!;

    [JsonIgnore]
    [ForeignKey("IdPg")]
    public virtual Pago IdPgNavigation { get; set; } = null!;

    [JsonIgnore]
    [ForeignKey("IdP,IdS,Fecha")]
    public virtual Sesion Sesion { get; set; } = null!;

    [JsonIgnore]
    [ForeignKey("IdB")]
    public virtual ICollection<Butaca> IdBs { get; set; } = new List<Butaca>();

    [JsonIgnore]
    [ForeignKey("IdD")]
    public virtual ICollection<Descuento> IdDs { get; set; } = new List<Descuento>();
}
