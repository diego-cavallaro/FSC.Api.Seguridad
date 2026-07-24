using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class MsStopCause
{
    public int Id { get; set; }

    public string Description { get; set; }

    public virtual ICollection<WorkOrderLog> WorkOrderLogs { get; set; } = new List<WorkOrderLog>();

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
