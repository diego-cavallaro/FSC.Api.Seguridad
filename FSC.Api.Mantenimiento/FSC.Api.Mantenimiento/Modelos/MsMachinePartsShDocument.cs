using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class MsMachinePartsShDocument
{
    public int Id { get; set; }

    public string MachineId { get; set; }

    public string PartId { get; set; }

    public string DocumentId { get; set; }

    public string DocumentDescription { get; set; }

    public string DocumentUrl { get; set; }

    public string Deleted { get; set; }
}
