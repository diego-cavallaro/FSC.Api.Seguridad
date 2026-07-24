using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class WorkOrderPart
{
    public long WoId { get; set; }

    public string MachineId { get; set; }

    public string PartId { get; set; }

    public virtual MsMachine Machine { get; set; }

    public virtual MsMachinePart Part { get; set; }

    public virtual WorkOrder Wo { get; set; }
}
