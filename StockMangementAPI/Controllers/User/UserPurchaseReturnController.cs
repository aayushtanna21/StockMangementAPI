using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data.User;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserPurchaseReturnController : ControllerBase
    {
        private readonly UserPurchaseReturnRepository _userpurchasereturnrepository;
        public UserPurchaseReturnController(UserPurchaseReturnRepository userpurchasereturnrepository)
        {
            _userpurchasereturnrepository = userpurchasereturnrepository;
        }
        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var stockTransactions = _userpurchasereturnrepository.SelectAll();
            return Ok(stockTransactions);
        }
        #endregion
        #region Product DropDown
        [HttpGet]
        public IActionResult ProductDropDown()
        {
            var stockTransactions = _userpurchasereturnrepository.ProductDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region Customer DropDown
        [HttpGet]
        public IActionResult CustomerDropDown()
        {
            var stockTransactions = _userpurchasereturnrepository.CustomerDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region Supplier DropDown
        [HttpGet]
        public IActionResult SuppliersDropDown()
        {
            var stockTransactions = _userpurchasereturnrepository.SuppliersDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region  GetByID
        [HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var stockTransactions = _userpurchasereturnrepository.SelectByID(ID);
            if (stockTransactions == null)
            {
                return NotFound();
            }
            return Ok(stockTransactions);
        }
        #endregion
        #region  Delete
        [HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var isDeleted = _userpurchasereturnrepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
        #region  Insert

        [HttpPost]
        public IActionResult Insert([FromBody] UserPurchaseReturnModel userPurchaseReturnModel)
        {
            if (userPurchaseReturnModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _userpurchasereturnrepository.Insert(userPurchaseReturnModel);
            if (isInserted)
            {
                return Ok(new { Message = "StockTransactions is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
        #endregion
        #region  Update
        [HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] UserPurchaseReturnModel userPurchaseReturnModel)
        {
            if (userPurchaseReturnModel == null || ID != userPurchaseReturnModel.PurchaseReturnID)
            {
                return BadRequest();
            }
            var isUpdated = _userpurchasereturnrepository.Update(userPurchaseReturnModel);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion
    }
}
