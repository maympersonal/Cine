using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Tarjetum
{
    public string CodigoT { get; set; } = null!;

    public string? Ci { get; set; }

    public virtual Cliente? CiNavigation { get; set; }

    public virtual ICollection<Web> Webs { get; set; } = new List<Web>();
}
