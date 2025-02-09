using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data.User;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserSalesReturnController : ControllerBase
    {
        private readonly UserSalesReturnRepository _usersalesreturnrepository;
        public UserSalesReturnController(UserSalesReturnRepository usersalesreturnrepository)
        {
            _usersalesreturnrepository = usersalesreturnrepository;
        }
        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var stockTransactions = _usersalesreturnrepository.SelectAll();
            return Ok(stockTransactions);
        }
        #endregion
        #region Product DropDown
        [HttpGet]
        public IActionResult ProductDropDown()
        {
            var stockTransactions = _usersalesreturnrepository.ProductDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region Customer DropDown
        [HttpGet]
        public IActionResult CustomerDropDown()
        {
            var stockTransactions = _usersalesreturnrepository.CustomerDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region  GetByID
        [HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var stockTransactions = _usersalesreturnrepository.SelectByID(ID);
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
            var isDeleted = _usersalesreturnrepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
        #region  Insert

        [HttpPost]
        public IActionResult Insert([FromBody] UserSalesReturnModel userSalesReturnModel)
        {
            if (userSalesReturnModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _usersalesreturnrepository.Insert(userSalesReturnModel);
            if (isInserted)
            {
                return Ok(new { Message = "StockTransactions is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
        #endregion
        #region  Update
        [HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] UserSalesReturnModel userSalesReturnModel)
        {
            if (userSalesReturnModel == null || ID != userSalesReturnModel.SalesReturnID)
            {
                return BadRequest();
            }
            var isUpdated = _usersalesreturnrepository.Update(userSalesReturnModel);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion
    }
}
