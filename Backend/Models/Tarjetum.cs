using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

public partial class Tarjetum
{
    [Column("CodigoT", TypeName = "varchar(18)")]
    public string CodigoT { get; set; } = null!;

    [Column("Ci", TypeName = "char(11)")]
    public string? Ci { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(Ci))]
    public virtual Cliente? CiNavigation { get; set; }

    [JsonIgnore]
    [InverseProperty(nameof(Web.CodigoTNavigation))]
    public virtual ICollection<Web> Webs { get; set; } = new List<Web>();
}