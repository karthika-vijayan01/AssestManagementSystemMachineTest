using System;
using System.Collections.Generic;

namespace AssestManagementSystemMachineTest.Models;

public partial class Vendor
{
    public int VId { get; set; }

    public string? VendorName { get; set; }

    public string? VendorType { get; set; }

    public int? AtId { get; set; }

    public DateTime? VendorFromDate { get; set; }

    public DateTime? VendorToDate { get; set; }

    public string? VendorAddress { get; set; }

    public virtual ICollection<AssetMain> AssetMains { get; set; } = new List<AssetMain>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
