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
    public class BillsController : ControllerBase
    {
        private readonly BillsRepository _billsRepository;
        #region Bills Construtor
        public BillsController(BillsRepository billsRepository)
        {
            _billsRepository = billsRepository;
        }
        #endregion
        #region Bills GetAll
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var bills = _billsRepository.SelectAll();
            return Ok(bills);
        }
        #endregion
        #region CustomerDropDown
        [HttpGet]
        public IActionResult CustomerDropDown()
        {
            var customer = _billsRepository.CustomerDropDown();
            return Ok(customer);
        }
        #endregion
        #region User DropDown
        [HttpGet]
        public IActionResult UserDropDown()
        {
            var user = _billsRepository.UserDropDown();
            return Ok(user);
        }
        #endregion
        [HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var bills = _billsRepository.SelectByID(ID);
            if (bills == null)
            {
                return NotFound();
            }
            return Ok(bills);
        }
        [HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var isDeleted = _billsRepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost]
        public IActionResult Insert([FromBody] BillsModel billsModel)
        {
            if (billsModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _billsRepository.Insert(billsModel);
            if (isInserted)
            {
                return Ok(new { Message = "Bills is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
        [HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] BillsModel billsModel)
        {
            if (billsModel == null || ID != billsModel.BillID)
            {
                return BadRequest();
            }
            var isUpdated = _billsRepository.Update(billsModel);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
