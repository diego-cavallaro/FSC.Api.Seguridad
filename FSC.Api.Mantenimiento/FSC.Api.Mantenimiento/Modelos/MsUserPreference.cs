using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class MsUserPreference
{
    public string IdUser { get; set; }

    public bool? ApplyStop { get; set; }

    public decimal? StopTime { get; set; }
}
