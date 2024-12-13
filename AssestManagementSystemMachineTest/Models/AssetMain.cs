using System;
using System.Collections.Generic;

namespace AssestManagementSystemMachineTest.Models;

public partial class AssetMain
{
    public int AssetId { get; set; }

    public string AssetName { get; set; } = null!;

    public int? VId { get; set; }

    public int? AssetDetailId { get; set; }

    public DateTime DateAdded { get; set; }

    public string Status { get; set; } = null!;

    public virtual AssetDetail? AssetDetail { get; set; }

    public virtual Vendor? VIdNavigation { get; set; }
}
