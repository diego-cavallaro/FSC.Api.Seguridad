using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class WorkOrderTask
{
    public long WoId { get; set; }

    public int TaskNo { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Observations { get; set; }

    public string User { get; set; }

    public DateTime? CloseDate { get; set; }

    public string CloseUser { get; set; }

    public virtual WorkOrder Wo { get; set; }

    public virtual ICollection<WorkOrderWorker> WorkOrderWorkers { get; set; } = new List<WorkOrderWorker>();
}
