using System;
using System.Collections.Generic;

namespace PUAM_1.Models;

public partial class Carrera
{
    public int IdCarrera { get; set; }

    public string CodigoCarrera { get; set; } = null!;

    public string NombreCarrera { get; set; } = null!;

    public string Facultad { get; set; } = null!;

    public virtual ICollection<Estudiante> Estudiantes { get; } = new List<Estudiante>();
}
