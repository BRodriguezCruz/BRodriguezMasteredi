using System;
using System.Collections.Generic;

namespace DL;

public partial class Emisor
{
    public string IdEmisor { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public DateTime FechaInicioOperacion { get; set; }

    public decimal Capital { get; set; }
}
