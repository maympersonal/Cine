using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Butaca")]
public partial class Butaca
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("IdB")]
    public int IdB { get; set; }

    [Required]
    [Column("IdS")]
    [ForeignKey("IdS")]
    public int IdS { get; set; }

    [JsonIgnore]
    ////[InverseProperty(nameof(Compra.IdBs))]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}