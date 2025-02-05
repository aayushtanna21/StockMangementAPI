using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentsRepository _paymentsRepository;
        public PaymentsController(PaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }
		#region Payments GetAll
		[HttpGet]
        public IActionResult GetAll()
        {
            var payments = _paymentsRepository.SelectAll();
            return Ok(payments);
        }
		#endregion
		#region Bills DropDown
		[HttpGet]
        public IActionResult BillsDropDown()
        {
            var bills = _paymentsRepository.BillsDropDown();
            return Ok(bills);
        }
		#endregion
		#region Payments GetByID

		[HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var payments = _paymentsRepository.SelectByID(ID);
            if (payments == null)
            {
                return NotFound();
            }
            return Ok(payments);
        }
		#endregion
		#region Payments Delete
		[HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var isDeleted = _paymentsRepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
		#endregion
		#region Payments Insert
		[HttpPost]
        public IActionResult Insert([FromBody] PaymentsModel paymentsModel)
        {
            if (paymentsModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _paymentsRepository.Insert(paymentsModel);
            if (isInserted)
            {
                return Ok(new { Message = "Payment is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
		#endregion
		#region Payments Update
		[HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] PaymentsModel paymentsModel)
        {
            if (paymentsModel == null || ID != paymentsModel.PaymentID)
            {
                return BadRequest();
            }
            var isUpdated = _paymentsRepository.Update(paymentsModel);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
		#endregion
	}
}
