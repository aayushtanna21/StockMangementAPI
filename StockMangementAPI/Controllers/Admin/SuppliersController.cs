using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data.Admin;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers.Admin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly SuppliersRepository _suppliersRepository;
        public SuppliersController(SuppliersRepository suppliersRepository)
        {
            _suppliersRepository = suppliersRepository;
        }
        #region Suppliers GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var suppliers = _suppliersRepository.SelectAll();
            return Ok(suppliers);
        }
        #endregion
        #region User DropDown
        [HttpGet]
        public IActionResult DropDown()
        {
            var suppliers = _suppliersRepository.UserDropDown();
            return Ok(suppliers);
        }
        #endregion
        #region Suppliers GetByID
        [HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var suppliers = _suppliersRepository.SelectByID(ID);
            if (suppliers == null)
            {
                return NotFound();
            }
            return Ok(suppliers);
        }
        #endregion
        #region Suppliers Delete
        [HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var isDeleted = _suppliersRepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
        #region Suppliers Insert
        [HttpPost]
        public IActionResult Insert([FromBody] SuppliersModel suppliersModel)
        {
            if (suppliersModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _suppliersRepository.Insert(suppliersModel);
            if (isInserted)
            {
                return Ok(new { Message = "Supplier is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
        #endregion
        #region Suppliers Update
        [HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] SuppliersModel suppliersModel)
        {
            if (suppliersModel == null || ID != suppliersModel.SupplierID)
            {
                return BadRequest();
            }
            var isUpdated = _suppliersRepository.Update(suppliersModel);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
    }
}
