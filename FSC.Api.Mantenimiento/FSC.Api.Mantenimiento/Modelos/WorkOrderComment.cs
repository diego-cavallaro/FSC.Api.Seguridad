using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class WorkOrderComment
{
    public long WoId { get; set; }

    public long Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public string User { get; set; }

    public string Comment { get; set; }

    public virtual WorkOrder Wo { get; set; }
}
