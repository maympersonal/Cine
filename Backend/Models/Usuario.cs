using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Usuario")]
public partial class Usuario
{
    [Column("Ci", TypeName = "char(11)")]
    public string Ci { get; set; } = null!;

    [Column("NombreS", TypeName = "varchar(50)")]
    public string? NombreS { get; set; }

    [Column("Apellidos", TypeName = "varchar(50)")]
    public string? Apellidos { get; set; }

    [Column("Puntos")]
    public int? Puntos { get; set; }

    [JsonIgnore]
    [Column("Codigo", TypeName = "varchar(11)")]
    public string? Codigo { get; set; }

    [JsonIgnore]
    [Column("Contrasena", TypeName = "binary(256)")]
    public byte[]? Contrasena { get; set; }

    [Column("Rol", TypeName = "varchar(10)")]
    public string? Rol { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(Ci))]
    public virtual Cliente CiNavigation { get; set; } = null!;
}