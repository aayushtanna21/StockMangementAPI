using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data.User;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserPurchaseController : ControllerBase
    {
        private readonly UserPurchaseRepository _userpurchaserepository;
        public UserPurchaseController(UserPurchaseRepository userpurchaserepository)
        {
            _userpurchaserepository = userpurchaserepository;
        }
        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var stockTransactions = _userpurchaserepository.SelectAll();
            return Ok(stockTransactions);
        }
        #endregion
        #region Product DropDown
        [HttpGet]
        public IActionResult ProductDropDown()
        {
            var stockTransactions = _userpurchaserepository.ProductDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region Customer DropDown
        [HttpGet]
        public IActionResult CustomerDropDown()
        {
            var stockTransactions = _userpurchaserepository.CustomerDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region Supplier DropDown
        [HttpGet]
        public IActionResult SuppliersDropDown()
        {
            var stockTransactions = _userpurchaserepository.SuppliersDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region  GetByID
        [HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var stockTransactions = _userpurchaserepository.SelectByID(ID);
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
            var isDeleted = _userpurchaserepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
        #region  Insert

        [HttpPost]
        public IActionResult Insert([FromBody] UserPurchaseModel userPurchaseModel)
        {
            if (userPurchaseModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _userpurchaserepository.Insert(userPurchaseModel);
            if (isInserted)
            {
                return Ok(new { Message = "StockTransactions is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
        #endregion
        #region  Update
        [HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] UserPurchaseModel userPurchaseModel)
        {
            if (userPurchaseModel == null || ID != userPurchaseModel.PurchaseID)
            {
                return BadRequest();
            }
            var isUpdated = _userpurchaserepository.Update(userPurchaseModel);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion
    }
}
