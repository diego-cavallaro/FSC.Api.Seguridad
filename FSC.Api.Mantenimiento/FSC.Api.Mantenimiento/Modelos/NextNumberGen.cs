using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class NextNumberGen
{
    public int Id { get; set; }

    public string TableName { get; set; }

    public string ColumnName { get; set; }

    public string Description { get; set; }

    public string AlphaPrefix { get; set; }

    public string AlphaSuffix { get; set; }

    public int? NextNumber { get; set; }
}
