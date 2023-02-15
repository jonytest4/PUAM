using System;
using System.Collections.Generic;

namespace PUAM_1.Models;

public partial class Clase
{
    public int IdClase { get; set; }

    public string NombreClase { get; set; } = null!;

    public virtual ICollection<Inscripcion> Inscripcions { get; } = new List<Inscripcion>();
}
