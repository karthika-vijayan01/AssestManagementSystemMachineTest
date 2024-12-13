using System;
using System.Collections.Generic;

namespace AssestManagementSystemMachineTest.Models;

public partial class AssetDefanition
{
    public int AdId { get; set; }

    public string? AssetName { get; set; }

    public int? AtId { get; set; }

    public string? AdClass { get; set; }

    public virtual AssetType? At { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
