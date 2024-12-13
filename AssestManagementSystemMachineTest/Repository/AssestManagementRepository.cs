using AssestManagementSystemMachineTest.Models;
using AssestManagementSystemMachineTest.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AssestManagementSystemMachineTest.Repository
{
    public class AssestManagementRepository : IAssestManagementRepository
    {
        private readonly AssestManagementExamContext _context;

        //Dependency Injection - constructor injection - to get all the resources from the DBContext.
        //otherwise , everytime we need a table or data, we need to create a object for everything. ie)25 tables, then 25 objects need to be created.
        public AssestManagementRepository(AssestManagementExamContext context)
        {
            _context = context; //_context - virtual
        }

        #region 1 - Get all Vendor -search all
        public async Task<ActionResult<IEnumerable<Vendor>>> GetPurchaseOrderaa()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Vendors.Include(emp => emp.PurchaseOrders).ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<Vendor>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region  2 - Get all using ViewModel
        public async Task<ActionResult<IEnumerable<AssestViewModel>>> GetViewModelAssest()
        {
            //LINQ
            try
            {
                if (_context != null)
                {
                   

                    //LINQ
                    return await (from e in _context.PurchaseOrders
                                  from d in _context.Vendors
                                  where e.VId == d.VId
                                  select new AssestViewModel
                                  {
                                      PurchareOrderNo = e.PurchareOrderNo,
                                      PurchaseQuantity = e.PurchaseQuantity,
                                      PurchaseStatus = e.PurchaseStatus,
                                      VendorName = d.VendorName,
                                      VendorAddress = d.VendorAddress,
                                      VendorFromDate = d.VendorFromDate,
                                      VendorToDate = d.VendorToDate,
                                      VendorType = d.VendorType,
                                      PurchaseDeliveryDate = e.PurchaseDeliveryDate
                                  }).ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<AssestViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region 3 - Search By Id
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrderById(int id)
        {
            try
            {
                if (_context != null)
                {
                    //var tblEmployees = await _context.TblEmployees.FFindAsync(id);
                    var purchase = await _context.PurchaseOrders.Include(e => e.VIdNavigation).FirstOrDefaultAsync(e => e.PId == id);
                    return purchase;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 4  - Insert an purcase -return purcase record
        public async Task<ActionResult<PurchaseOrder>> PostPurchaseOrderReturnRecord(PurchaseOrder purcase)
        {
            try
            {
                //check if employee object is not null
                if (purcase == null)
                {
                    throw new ArgumentException(nameof(purcase), "Employee data is null");
                    //return null;
                }
                //Ensure the context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                //Add the employee record to the DBcontext
                await _context.PurchaseOrders.AddAsync(purcase);

                //save changes to the database
                await _context.SaveChangesAsync();

                //Retrieve the employee with the related department
                var employeeWithDepartment = await _context.PurchaseOrders.Include(e => e.VIdNavigation) //Eager load
                    .FirstOrDefaultAsync(e => e.PId == purcase.PId);

                //Return the added employee with the record added
                return employeeWithDepartment;
            }

            catch (Exception ex)
            {
                //Log exception here if needed
                return null;
            }
        }
        #endregion



        #region  6  - Update an Vendor with ID and Vendor
        public async Task<ActionResult<Vendor>> PutVendor(int id, Vendor vendor)
        {
            try
            {
                if (vendor == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                //Find the employee by id
                var existingvendor = await _context.Vendors.FindAsync(id);
                if (existingvendor == null)
                {
                    return null;
                }

                //Map values wit fields
                existingvendor.VendorName = vendor.VendorName;
                existingvendor.VendorType = vendor.VendorType;
                existingvendor.AtId = vendor.AtId;
                existingvendor.VendorFromDate = vendor.VendorFromDate;
                existingvendor.VendorToDate = vendor.VendorToDate;
                existingvendor.VendorAddress = vendor.VendorAddress;
                

                //save changes to the database
                await _context.SaveChangesAsync();

                //Retreive the employee with the related Department
                var vendorWithpurchase = await _context.Vendors.Include(e => e.AssetMains) //Eager load
                    .FirstOrDefaultAsync(existingvendor => existingvendor.VId == vendor.VId);

                //Return the added employee record
                return vendorWithpurchase;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  7  - Delete 
        public JsonResult Delete(int id)
        {
            try
            {
                if (id <= null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid Employee Id"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                //Ensure the context is not null
                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized"
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                //Find the employee by id
                var existingvendor = _context.Vendors.Find(id);

                if (existingvendor == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "vendor not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //Remove the employee record from the DBContext
                _context.Vendors.Remove(existingvendor);

                //save changes to the database
                _context.SaveChangesAsync();
                return new JsonResult(new
                {
                    success = true,
                    message = "vendor Deleted successfully"
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database context is not initialized"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion

        #region    8  - Get all PurchaseOrder
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>>GetPurchaseOrder()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.PurchaseOrders.ToListAsync();
                }
                //Returns an empty list if context is null
                return new List<PurchaseOrder>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region 9 - Using Stored Procedure
        public async Task<ActionResult<IEnumerable<Vendor>>> PostVendorProcedureReturnRecord(Vendor vendor)
        {
            try
            {
                //check if employee object is not null
                if (vendor == null)
                {
                    throw new ArgumentException(nameof(vendor), "Employee data is null");
                    //return null;
                }
                //Ensure the context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                //Add the employee record to the DBcontext
                var result = await _context.Vendors.FromSqlRaw(

                "EXEC InsertVendor @Vendor_Name, @Vendor_Type, @At_Id, @VendorFrom_Date, @VendorTo_Date, @Vendor_Address",


                new SqlParameter("@Vendor_Name", vendor.VendorName),
                new SqlParameter("@Vendor_Type", vendor.VendorType),
                new SqlParameter("@At_Id", vendor.AtId),
                new SqlParameter("@VendorFrom_Date", vendor.VendorFromDate),
                new SqlParameter("@VendorTo_Date", vendor.VendorToDate),
                new SqlParameter("@Vendor_Address", vendor.VendorAddress)
                                                                     ).ToListAsync();

                //save changes to the database
                if (result != null && result.Count > 0)
                {
                    return result;
                }
                else
                {
                    return null;
                }
                //Return the added employee with the record added

            }

            catch (Exception ex)
            {
                //Log exception here if needed
                return null;
            }




        }
        #endregion

        ///-------------------------------------------------AssetMain

        public async Task<AssetMain> createAssetmain(AssetMain main)
        {
            try
            {
                if (main == null)
                {
                    throw new ArgumentNullException(nameof(main), "AssetMain data is null");

                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }
                await _context.AssetMains.AddAsync(main);

               
                await _context.SaveChangesAsync();
                var asmain = await _context.AssetMains.Include(e => e.VIdNavigation)
                    .Include(e => e.AssetDetail)
                    .Include(e => e.AssetName)
                    .Include(e => e.DateAdded)
                    .FirstOrDefaultAsync(e => e.AssetId == main.AssetId);
                return asmain;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<IEnumerable<AssetMain>>> GetAllAssetsMain()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.AssetMains.Include(v => v.AssetName).
                        Include(v => v.AssetDetail).
                        Include(v => v.AssetDetailId).
                         Include(v => v.VId).ToListAsync();
                }

             
                return new List<AssetMain>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<AssetMain>> UpdateAssetMain(int id, AssetMain main)
        {
            try
            {
                if (main == null)
                {
                    throw new ArgumentNullException(nameof(main), "AssetMain data is null");

                }
               
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }
                
                var existing = await _context.AssetMains.FindAsync(id);
                if (existing == null)
                {
                    return null;
                }

                
                existing.DateAdded = main.DateAdded;
                existing.Status = main.Status;




               
                await _context.SaveChangesAsync();
                
                var asmain = await _context.AssetMains.Include(e => e.AssetName)
                    .Include(e => e.AssetDetail)
                    .Include(e => e.AssetDetailId)
                    .Include(e => e.VId)
                    .FirstOrDefaultAsync(e => e.AssetId == main.AssetId);
                return asmain;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult DeleteAssetMain(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid AssetMain id"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                }
                
                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database coontext is not initialized"

                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                
                var existing = _context.AssetMains.Find(id);
                if (existing == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "AssetMain  not found"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //remove

                _context.AssetMains.Remove(existing);

                _context.SaveChangesAsync();


                return new JsonResult(new
                {
                    success = true,
                    message = "AssetMains deleted successfully"

                })
                {
                    StatusCode = StatusCodes.Status200OK
                };

            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database coontext is not initialized"

                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

        }
        #region
        public async Task<ActionResult<AssetMain>> SearchAssetMainById(int id)
        {
            try
            {
                if (_context != null)
                {
                    // find the employee by id 
                    var order = await _context.AssetMains
                    .Include(ass => ass.AssetName)
                     .Include(ass => ass.AssetId)
                      .Include(ass => ass.AssetDetailId)
                      .Include(ass => ass.VId)
                    .FirstOrDefaultAsync(e => e.AssetId == id);
                    return order;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
#endregion
    }
}


