using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class MsMachinesOri
{
    public string Id { get; set; }

    public string Description { get; set; }

    public int? IdCategory { get; set; }

    public int? Status { get; set; }

    public int? Order { get; set; }

    public bool? Inviewer { get; set; }

    public string Deleted { get; set; }

    public string VmId { get; set; }

    public string GroupId { get; set; }

    public int? Criticidad { get; set; }

    public int? Regimen { get; set; }
}
