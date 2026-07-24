using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class WorkOrderLog
{
    public long Id { get; set; }

    public long WoId { get; set; }

    public DateTime? CreateDate { get; set; }

    public string MachineId { get; set; }

    public string MaintenanceType { get; set; }

    public DateTime? ProgrammingDate { get; set; }

    public string AssignedTo { get; set; }

    public string User { get; set; }

    public string Status { get; set; }

    public string Observations { get; set; }

    public string JobType { get; set; }

    public int? IdStopCause { get; set; }

    public string UserCloser { get; set; }

    public string UserCanceller { get; set; }

    public string PartList { get; set; }

    public DateTime? FechaUpdate { get; set; }

    public string UsuarioUpdate { get; set; }

    public virtual MsStopCause IdStopCauseNavigation { get; set; }

    public virtual MsMachine Machine { get; set; }

    public virtual WorkOrder Wo { get; set; }
}
