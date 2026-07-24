using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class WorkOrderManager
{
    public long WoId { get; set; }

    public string User { get; set; }

    public DateTime? JoinDate { get; set; }

    public virtual WorkOrder Wo { get; set; }
}
