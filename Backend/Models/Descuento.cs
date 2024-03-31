using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Descuento")]
public partial class Descuento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("IdD")]
    public int IdD { get; set; }

    [Column("NombreD",TypeName = "varchar(30)")]
    public string? NombreD { get; set; }

    [Column("Porciento")]
    public double? Porciento { get; set; }

    [JsonIgnore]
    [InverseProperty(nameof(Compra.IdDs))]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
