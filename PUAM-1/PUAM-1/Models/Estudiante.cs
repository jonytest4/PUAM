using System;
using System.Collections.Generic;

namespace PUAM_1.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public int Carrera { get; set; }

    public int Semestre { get; set; }

    public string Coordinador { get; set; } = null!;

    public string Clave { get; set; } = null!;
    public string? ConfirmarClave { get; set; }
    public virtual Carrera CarreraNavigation { get; set; } = null!;

    public virtual Registro? Registro { get; set; }
}
