using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Sesion")]
public partial class Sesion
{
    [Column("IdP")]
    public int IdP { get; set; }

    [Column("IdS")]
    public int IdS { get; set; }

    [Column("Fecha")]
    public DateTime Fecha { get; set; }

    [JsonIgnore]
    //[InverseProperty(nameof(Compra.Sesion))]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    [JsonIgnore]
    [ForeignKey(nameof(IdP))]
    public virtual Pelicula IdPNavigation { get; set; } = null!;

    [JsonIgnore]
    [ForeignKey(nameof(IdS))]
    public virtual Sala IdSNavigation { get; set; } = null!;
}