using System;
using System.Collections.Generic;

namespace AssestManagementSystemMachineTest.Models;

public partial class PurchaseOrder
{
    public int PId { get; set; }

    public string? PurchareOrderNo { get; set; }

    public int? AdId { get; set; }

    public int? AtId { get; set; }

    public decimal? PurchaseQuantity { get; set; }

    public int? VId { get; set; }

    public DateTime? PurchaseOrderDate { get; set; }

    public DateTime? PurchaseDeliveryDate { get; set; }

    public string? PurchaseStatus { get; set; }

    public virtual Vendor? VIdNavigation { get; set; }
}

