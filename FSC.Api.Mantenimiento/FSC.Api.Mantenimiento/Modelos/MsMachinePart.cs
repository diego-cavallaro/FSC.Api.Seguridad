using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class MsMachinePart
{
    public string MachineId { get; set; }

    public string PartId { get; set; }

    public string FatherPartId { get; set; }

    public string Description { get; set; }

    public string Deleted { get; set; }

    public virtual MsMachinePart FatherPart { get; set; }

    public virtual ICollection<MsMachinePart> InverseFatherPart { get; set; } = new List<MsMachinePart>();

    public virtual MsMachine Machine { get; set; }

    public virtual ICollection<WorkOrderPart> WorkOrderParts { get; set; } = new List<WorkOrderPart>();
}
