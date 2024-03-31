using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Cliente")]
public partial class Cliente
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [StringLength(11)]
    [Column("Ci",TypeName ="char(11)")]
    public string Ci { get; set; } = null!;
    
    [Column("Correo",TypeName ="varchar(256)")]
    [EmailAddress]
    public string? Correo { get; set; }

    [Column("Confiabilidad")]
    public bool? Confiabilidad { get; set; }

    [JsonIgnore]
    [InverseProperty("CiNavigation")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    [JsonIgnore]
    [InverseProperty("CiNavigation")]
    public virtual ICollection<Tarjetum> Tarjeta { get; set; } = new List<Tarjetum>();

    [JsonIgnore]
    [InverseProperty("CiNavigation")]
    public virtual Usuario? Usuario { get; set; }
}
