using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class WorkOrderWorker
{
    public long WoId { get; set; }

    public int TaskNo { get; set; }

    public string EmployeeId { get; set; }

    public decimal? WorkedHours { get; set; }

    public string User { get; set; }

    public decimal? ExtraHours { get; set; }

    public decimal? HighHours { get; set; }

    public decimal? DepthHours { get; set; }

    public virtual WorkOrderTask WorkOrderTask { get; set; }
}
