using System;
using System.Collections.Generic;

namespace AssestManagementSystemMachineTest.Models;

public partial class AssetType
{
    public int AtId { get; set; }

    public string? AtName { get; set; }

    public virtual ICollection<AssetDefanition> AssetDefanitions { get; set; } = new List<AssetDefanition>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
