using AssestManagementSystemMachineTest.Models;
using AssestManagementSystemMachineTest.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AssestManagementSystemMachineTest.Repository
{
    public interface IAssestManagementRepository
    {

        #region 1 -  Get all Vendor from DB - Search All
        public Task<ActionResult<IEnumerable<Vendor>>> GetPurchaseOrderaa();
        #endregion

        #region  2 - Get all AssestViewModel
        public Task<ActionResult<IEnumerable<AssestViewModel>>> GetViewModelAssest();
        #endregion

        #region   3 - Get an PurchaseOrder based on Id
        public Task<ActionResult<PurchaseOrder>> GetPurchaseOrderById(int id);
        #endregion

        #region  4  - Insert an PurchaseOrder 
        public Task<ActionResult<PurchaseOrder>> PostPurchaseOrderReturnRecord(PurchaseOrder purchase);
        #endregion

        #region  6  - Update an Vendor with ID and Vendor
        public Task<ActionResult<Vendor>> PutVendor(int id, Vendor vendor);
        #endregion

        #region 7  - Delete an 
        public JsonResult Delete(int id);
        #endregion

        #region 8  - Get all PurchaseOrder
        public Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrder();
        #endregion

        #region 9 - Using Stored Procedure
        public Task<ActionResult<IEnumerable<Vendor>>> PostVendorProcedureReturnRecord(Vendor vendor);
        #endregion


        //---------------------------------------------------

        #region 1 - Insert an AssetMain 
        public Task<AssetMain> createAssetmain(AssetMain main);
        #endregion

        #region  2 - Get an AssetMain
        public Task<ActionResult<IEnumerable<AssetMain>>> GetAllAssetsMain();
        #endregion

        #region   3 - Update an AssetMain
        public Task<ActionResult<AssetMain>> UpdateAssetMain(int id, AssetMain main);
        #endregion

        #region  4  - Delete an AssetMain 
        public JsonResult DeleteAssetMain(int id);
        #endregion

        #region 5 - Search an AssetMain 
        public Task<ActionResult<AssetMain>> SearchAssetMainById(int id);
        #endregion



    }
}
