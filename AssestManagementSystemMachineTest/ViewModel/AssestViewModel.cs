namespace AssestManagementSystemMachineTest.ViewModel
{
    public class AssestViewModel
    {
        public string? PurchareOrderNo { get; set; }
        public decimal? PurchaseQuantity { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }

        public DateTime? PurchaseDeliveryDate { get; set; }

        public string? PurchaseStatus { get; set; }
        public string? VendorName { get; set; }

        public string? VendorType { get; set; }

        public DateTime? VendorFromDate { get; set; }

        public DateTime? VendorToDate { get; set; }

        public string? VendorAddress { get; set; }


        public string? AtName { get; set; }

        public string AssetName { get; set; } = null!;



    }
}
