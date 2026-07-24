using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class WorkOrderStatusChange
{
    public long WoId { get; set; }

    public DateTime WoCloseDate { get; set; }

    public string WoStatus { get; set; }

    public string MsMachineId { get; set; }

    public string MsGroupId { get; set; }
}
