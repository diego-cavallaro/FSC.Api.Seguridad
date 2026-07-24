using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class MachineDependence
{
    public string MachineId { get; set; }

    public string FatherMachineId { get; set; }

    public bool? DualDependence { get; set; }

    public virtual MachineDependence FatherMachine { get; set; }

    public virtual ICollection<MachineDependence> InverseFatherMachine { get; set; } = new List<MachineDependence>();
}
