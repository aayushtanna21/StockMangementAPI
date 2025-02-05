using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        #region User Construtor
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
		#endregion
		#region User GetAll
		[HttpGet]
        public IActionResult GetAll()
        {
            var user = _userRepository.SelectAll();
            return Ok(user);
        }
		#endregion
		#region User GetByID
		[HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var user = _userRepository.SelectByID(ID);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
		#endregion
		#region User Delete
		[HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var isDeleted = _userRepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
		#endregion
		#region User Insert
		[HttpPost]
        public IActionResult Insert([FromBody] UserModel userModel)
        {
            if (userModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _userRepository.Insert(userModel);
            if (isInserted)
            {
                return Ok(new { Message = "User is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
		#endregion
		#region User Update
		[HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] UserModel userModel)
        {
            if (userModel == null || ID != userModel.UserID)
            {
                return BadRequest();
            }
            var isUpdated = _userRepository.Update(userModel);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
		#endregion
	}
}
