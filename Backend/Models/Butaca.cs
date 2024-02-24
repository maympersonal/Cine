using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Butaca
{
    public int IdB { get; set; }

    public int IdS { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
