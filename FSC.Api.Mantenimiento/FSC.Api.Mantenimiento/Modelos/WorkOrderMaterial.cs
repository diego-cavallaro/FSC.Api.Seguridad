using System;
using System.Collections.Generic;

namespace FSC.Api.Mantenimiento.Modelos;

public partial class WorkOrderMaterial
{
    public long Id { get; set; }

    public long WoId { get; set; }

    public string PartId { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string StockUm { get; set; }

    public string Observations { get; set; }

    public string User { get; set; }

    public virtual WorkOrder Wo { get; set; }
}
