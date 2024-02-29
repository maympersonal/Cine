using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Empleado
{
    public int IdE { get; set; }

    public bool? Gerente { get; set; }

    public byte[]? ContrasenaE { get; set; }
}
