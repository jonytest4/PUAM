using System;
using System.Collections.Generic;

namespace PUAM_1.Models;

public partial class Registro
{
    public int IdRegistro { get; set; }

    public string CedulaEstudiante { get; set; } = null!;

    public DateTime? Fecha { get; set; }

    public int? NumeroHoras { get; set; }

    public string CedulaAdulto { get; set; } = null!;

    public string? Evidencia { get; set; }

    public string? Observaciones { get; set; }

    public virtual Adulto CedulaAdultoNavigation { get; set; } = null!;

    public virtual Estudiante CedulaEstudianteNavigation { get; set; } = null!;
}
