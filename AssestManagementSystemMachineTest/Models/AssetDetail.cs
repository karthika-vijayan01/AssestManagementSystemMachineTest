using System;
using System.Collections.Generic;

namespace AssestManagementSystemMachineTest.Models;

public partial class AssetDetail
{
    public int AssetDetailId { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string Specification { get; set; } = null!;

    public string WarrantyPeriod { get; set; } = null!;

    public DateTime? FromDate { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<AssetMain> AssetMains { get; set; } = new List<AssetMain>();
}
