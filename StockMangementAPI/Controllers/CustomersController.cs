using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersRepository _customersRepository;
        public CustomersController(CustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }
		#region Customers GetAll
		[HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customersRepository.SelectAll();
            return Ok(customers);
        }
		#endregion
		#region User DropDown
		[HttpGet]
        public IActionResult UserDropDown()
        {
            var user=_customersRepository.UserDropDown();
            return Ok(user);
        }
		#endregion
		#region Customers GetByID
		[HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var customers = _customersRepository.SelectByID(ID);
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
		#endregion
		#region Customers Delete		
        [HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var isDeleted = _customersRepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
		#endregion
		#region Customers Insert
		[HttpPost]
        public IActionResult Insert([FromBody] CustomersModel customersModel)
        {
            if (customersModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _customersRepository.Insert(customersModel);
            if (isInserted)
            {
                return Ok(new { Message = "Customers is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
		#endregion
		#region Customers Update
		[HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] CustomersModel customersModel)
        {
            if (customersModel == null || ID != customersModel.CustomerID)
            {
                return BadRequest();
            }
            var isUpdated = _customersRepository.Update(customersModel);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
		#endregion
	}
}
