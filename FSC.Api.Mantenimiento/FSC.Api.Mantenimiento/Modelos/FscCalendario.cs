using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class FscCalendario
{
    public long Id { get; set; }

    public DateTime Cfecha { get; set; }

    public string CdiaSemana { get; set; }

    public string Cferiado { get; set; }

    public decimal Cporcentaje { get; set; }
}
