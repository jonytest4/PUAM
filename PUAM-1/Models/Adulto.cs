using System;
using System.Collections.Generic;

namespace PUAM_1.Models;

public partial class Adulto
{
    public int IdAdultos { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string Cedula { get; set; } = null!;

    public int? Edad { get; set; }

    public string? Celular { get; set; }

    public int? IdPrograma { get; set; }

    public string? Clave { get; set; }
    public string? ConfirmarClave { get; set; }

    public virtual Programa? IdProgramaNavigation { get; set; }

    public virtual Inscripcion? Inscripcion { get; set; }

    public virtual ICollection<Registro> Registros { get; } = new List<Registro>();
}
