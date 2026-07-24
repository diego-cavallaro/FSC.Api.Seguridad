using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class WorkOrder
{
    public long Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public string MachineId { get; set; }

    public string PartId { get; set; }

    public DateTime? ProgrammingDate { get; set; }

    public string MaintenanceType { get; set; }

    public string AssignedTo { get; set; }

    public string User { get; set; }

    public string Status { get; set; }

    public string Observations { get; set; }

    public string JobType { get; set; }

    public int? IdStopCause { get; set; }

    public string UserCloser { get; set; }

    public string UserCanceller { get; set; }

    public long? WoFatherId { get; set; }

    public string PartList { get; set; }

    public DateTime? FechaUpdate { get; set; }

    public string UsuarioUpdate { get; set; }

    public bool? Notificado { get; set; }

    public virtual MsStopCause IdStopCauseNavigation { get; set; }

    public virtual ICollection<WorkOrder> InverseWoFather { get; set; } = new List<WorkOrder>();

    public virtual MsMachine Machine { get; set; }

    public virtual WorkOrder WoFather { get; set; }

    public virtual ICollection<WorkOrderComment> WorkOrderComments { get; set; } = new List<WorkOrderComment>();

    public virtual ICollection<WorkOrderLog> WorkOrderLogs { get; set; } = new List<WorkOrderLog>();

    public virtual ICollection<WorkOrderMaterial> WorkOrderMaterials { get; set; } = new List<WorkOrderMaterial>();

    public virtual ICollection<WorkOrderPart> WorkOrderParts { get; set; } = new List<WorkOrderPart>();

    public virtual ICollection<WorkOrderTask> WorkOrderTasks { get; set; } = new List<WorkOrderTask>();

    public virtual ICollection<MsStopHistory> Stops { get; set; } = new List<MsStopHistory>();
}
