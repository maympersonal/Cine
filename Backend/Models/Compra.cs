using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Compra
{
    public int IdP { get; set; }

    public int IdS { get; set; }

    public DateTime Fecha { get; set; }

    public string Ci { get; set; } = null!;

    public int IdPg { get; set; }

    public string? Tipo { get; set; }

    public DateTime FechaDeCompra { get; set; }

    public string? MedioAd { get; set; }

    [JsonIgnore]
    public virtual Cliente CiNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Pago IdPgNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Sesion Sesion { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Butaca> IdBs { get; set; } = new List<Butaca>();

    [JsonIgnore]
    public virtual ICollection<Descuento> IdDs { get; set; } = new List<Descuento>();
}
