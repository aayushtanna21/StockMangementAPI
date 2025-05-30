using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using StockMangementAPI.Data.Admin;

namespace StockMangementAPI.Controllers.Admin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BillDetailsController : ControllerBase
    {
        private readonly BillDetailsRepository _billdetailsRepository;
        #region BillsDeatils Constructor 
        public BillDetailsController(BillDetailsRepository billdetailsRepository)
        {
            _billdetailsRepository = billdetailsRepository;
        }
        #endregion
        #region BilsDeatils GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var billDetails = _billdetailsRepository.SelectAll();
            return Ok(billDetails);
        }
        #endregion
        #region BillDropDown
        [HttpGet]
        public IActionResult BillDropDown()
        {
            var bills = _billdetailsRepository.BillsDropDown();
            return Ok(bills);
        }
        #endregion
        #region ProductDropDown
        [HttpGet]
        public IActionResult ProductDropDown()
        {
            var product = _billdetailsRepository.ProductDropDown();
            return Ok(product);
        }
        #endregion
        #region  BilsDeatils GetByID
        [HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var billDetails = _billdetailsRepository.SelectByID(ID);
            if (billDetails == null)
            {
                return NotFound();
            }
            return Ok(billDetails);
        }
        #endregion
        #region BilsDeatils Delete
        [HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var isDeleted = _billdetailsRepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
        #region BilsDeatils Insert
        [HttpPost]
        public IActionResult Insert([FromBody] BillDetailsModel billDetailsModel)
        {
            if (billDetailsModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _billdetailsRepository.Insert(billDetailsModel);
            if (isInserted)
            {
                return Ok(new { Message = "BillDetails is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
        #endregion
        #region BillsDetail Update
        [HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] BillDetailsModel billDetailsModel)
        {
            if (billDetailsModel == null || ID != billDetailsModel.BillDetailID)
            {
                return BadRequest();
            }
            var isUpdated = _billdetailsRepository.Update(billDetailsModel);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
    }
}
