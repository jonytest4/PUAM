using System;
using System.Collections.Generic;

namespace PUAM_1.Models;

public partial class Programa
{
    public int IdPrograma { get; set; }

    public string Programa1 { get; set; } = null!;

    public virtual ICollection<Adulto> Adultos { get; } = new List<Adulto>();
}
