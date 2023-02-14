using System;
using System.Collections.Generic;

namespace PUAM_1.Models;

public partial class Inscripcion
{
    public int IdInscripcion { get; set; }

    public string CedulaAdulto { get; set; } = null!;

    public int IdClase { get; set; }

    public virtual Adulto CedulaAdultoNavigation { get; set; } = null!;

    public virtual Clase IdClaseNavigation { get; set; } = null!;
}
