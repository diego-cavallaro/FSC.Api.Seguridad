using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class WorkOrderMaintenanceType
{
    public string Id { get; set; }

    public int WId { get; set; }

    public int? Proyectado { get; set; }
}
