using AssestManagementSystemMachineTest.Models;
using AssestManagementSystemMachineTest.Repository;
using AssestManagementSystemMachineTest.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssestManagementSystemMachineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")] 
    public class AssestManagementsController : Controller
    {
            private readonly IAssestManagementRepository _repository;

            public AssestManagementsController(IAssestManagementRepository repository)
            {
                _repository = repository;
            }

           

            #region 1 - Get all employees - search all
            [HttpGet]
           [Authorize(AuthenticationSchemes = "Bearer")]
            public async Task<ActionResult<IEnumerable<Vendor>>> GetPurchaseOrderaa()
            {
                var employees = await _repository.GetPurchaseOrder();
                if (employees == null)
                {
                    return NotFound("No Vendor found");
                }
                return Ok(employees);
            }

            #endregion

            #region 2 - Get all from viewModel 
            [HttpGet("vm")]
            public async Task<ActionResult<IEnumerable<AssestViewModel>>> GetViewModelAssest()
            {
                var employees = await _repository.GetViewModelAssest();
                if (employees == null)
                {
                    return NotFound("No AssestViewModel found");
                }
                return Ok(employees);
            }

        #endregion

        #region 3 - Get Emploees - Search By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrderById(int id)
        {
            var employees = await _repository.GetPurchaseOrderById(id);
            if (employees == null)
            {
                return NotFound("No PurchaseOrder found");
            }
            return Ok(employees);
        }

        #endregion

        #region   4  - Insert an Employee -return employee record
        [HttpPost("insertP")]
        public async Task<ActionResult<PurchaseOrder>> PostPurchaseOrderReturnRecord(PurchaseOrder emp)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee
                var newEmployee = await _repository.PostPurchaseOrderReturnRecord(emp);
                if (newEmployee != null)
                {
                    return Ok(newEmployee);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion



        #region    6  - Update an Vendor with ID and Vendor
        [HttpPut("{id}/vendor")]
        public async Task<ActionResult<Vendor>> PutVendor(int id, Vendor emp)
        {
            if (ModelState.IsValid)
            {
                var updateVendor = await _repository.PutVendor(id, emp);
                if (updateVendor != null)
                {
                    return Ok(updateVendor);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region  7  - Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _repository.Delete(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "PurchaseOrder could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion

        #region   8 - Get all departments - search all
        [HttpGet("v2")]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrder()
        {
            var depts = await _repository.GetPurchaseOrder();
            if (depts == null)
            {
                return NotFound("No PurchaseOrder found");
            }
            return Ok(depts);
        }

        #endregion


        #region   9  - Insert an Vendor -return Vendor record
        [HttpPost("p1")]
        public async Task<ActionResult<IEnumerable<Vendor>>> PostVendorProcedureReturnRecord(Vendor emp)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee
                var newEmployee = await _repository.PostVendorProcedureReturnRecord(emp);
                if (newEmployee != null)
                {
                    return Ok(newEmployee);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        //-------------------------------------------AssetMain
        #region Get All Asset Types
        [HttpGet("p3")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<AssetMain>>> GetAllAssetsMain()
        {
            var assetTypes = await _repository.GetAllAssetsMain();
            if (assetTypes == null)
            {
                return NotFound("No asset types found.");
            }
            return Ok(assetTypes);
        }
        #endregion

        #region Add New Asset main
        [HttpPost("v2")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<AssetMain>> createAssetmain(AssetMain asset)
        {
            if (ModelState.IsValid)
            {
                var newAssetType = await _repository.createAssetmain(asset);
                if (newAssetType != null)
                {
                    return Ok(newAssetType);
                }
                else
                {
                    return NotFound("Asset type could not be added.");
                }
            }
            return BadRequest("Invalid asset type data.");
        }
        #endregion


        #region Update Asset main
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<AssetMain>> UpdateAssetMain(int id, AssetMain assetMain)
        {
            if (ModelState.IsValid)
            {
                var updatedAssetType = await _repository.UpdateAssetMain(id, assetMain);
                if (updatedAssetType != null)
                {
                    return Ok(updatedAssetType);
                }
                else
                {
                    return NotFound("Asset type could not be updated.");
                }
            }
            return BadRequest("Invalid asset type data.");
        }
        #endregion


        #region Delete Asset main
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult DeleteAssetMain(int id)
        {
            try
            {
                var result = _repository.DeleteAssetMain(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "mainassets could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion

        #region Search by id
        [HttpGet("{id1}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<AssetMain>> SearchAssetMainById(int id)
        {
            var order = await _repository.SearchAssetMainById(id);
            if (order == null)
            {
                return NotFound("No assets found ");
            }
            return Ok(order);
        }
        #endregion


    }
}
