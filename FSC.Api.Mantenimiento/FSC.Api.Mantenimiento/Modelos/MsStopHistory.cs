using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class MsStopHistory
{
    public int Id { get; set; }

    public string IdMachine { get; set; }

    public DateTime? Stop { get; set; }

    public DateTime? Start { get; set; }

    public string Comments { get; set; }

    public int? Status { get; set; }

    public string User { get; set; }

    public bool? Stopgen { get; set; }

    public string Deleted { get; set; }

    public virtual MsMachine IdMachineNavigation { get; set; }

    public virtual ICollection<WorkOrder> Wos { get; set; } = new List<WorkOrder>();
}
