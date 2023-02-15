using System;
using System.Collections.Generic;

namespace PUAM_1.Models;

public partial class Certificado
{
    public int IdCertificado { get; set; }

    public string? Estudiante { get; set; }

    public string? Coordinador { get; set; }

    public DateTime? FechaEmision { get; set; }

    public string CedulaEstudiante { get; set; } = null!;

    public virtual Estudiante CedulaEstudianteNavigation { get; set; } = null!;
}
