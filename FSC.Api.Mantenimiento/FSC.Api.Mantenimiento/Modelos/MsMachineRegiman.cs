using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class MsMachineRegiman
{
    public int Id { get; set; }

    public string Description { get; set; }

    public DateTime? Desde { get; set; }

    public DateTime? Hasta { get; set; }
}
