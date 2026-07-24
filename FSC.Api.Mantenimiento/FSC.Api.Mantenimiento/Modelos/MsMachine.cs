using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class MsMachine
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

    public bool Disparador { get; set; }

    public virtual ICollection<MsMachinePart> MsMachineParts { get; set; } = new List<MsMachinePart>();

    public virtual ICollection<MsStopHistory> MsStopHistories { get; set; } = new List<MsStopHistory>();

    public virtual ICollection<WorkOrderLog> WorkOrderLogs { get; set; } = new List<WorkOrderLog>();

    public virtual ICollection<WorkOrderPart> WorkOrderParts { get; set; } = new List<WorkOrderPart>();

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
