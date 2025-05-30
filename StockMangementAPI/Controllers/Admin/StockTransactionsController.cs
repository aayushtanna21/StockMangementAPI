using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data.Admin;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers.Admin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class StockTransactionsController : ControllerBase
    {
        private readonly StockTransactionsRepository _stocktransactionsRepository;
        public StockTransactionsController(StockTransactionsRepository stocktransactionsRepository)
        {
            _stocktransactionsRepository = stocktransactionsRepository;
        }
        #region StockTransactions GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var stockTransactions = _stocktransactionsRepository.SelectAll();
            return Ok(stockTransactions);
        }
        #endregion
        #region Product DropDown
        [HttpGet]
        public IActionResult ProductDropDown()
        {
            var stockTransactions = _stocktransactionsRepository.ProductDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region User DropDown
        [HttpGet]
        public IActionResult UserDropDown()
        {
            var stockTransactions = _stocktransactionsRepository.UserDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region StockTransactions GetByID
        [HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var stockTransactions = _stocktransactionsRepository.SelectByID(ID);
            if (stockTransactions == null)
            {
                return NotFound();
            }
            return Ok(stockTransactions);
        }
        #endregion
        #region StockTransactions Delete
        [HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var isDeleted = _stocktransactionsRepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
        #region StockTransactions Insert

        [HttpPost]
        public IActionResult Insert([FromBody] StockTransactionsModel stockTransactionsModel)
        {
            if (stockTransactionsModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _stocktransactionsRepository.Insert(stockTransactionsModel);
            if (isInserted)
            {
                return Ok(new { Message = "StockTransactions is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
        #endregion
        #region StockTransactions Update
        [HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] StockTransactionsModel stockTransactionsModel)
        {
            if (stockTransactionsModel == null || ID != stockTransactionsModel.StockTransactionID)
            {
                return BadRequest();
            }
            var isUpdated = _stocktransactionsRepository.Update(stockTransactionsModel);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion
    }
}
