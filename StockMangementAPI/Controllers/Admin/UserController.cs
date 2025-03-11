using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockMangementAPI.Data.Admin;
using StockMangementAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace StockMangementAPI.Controllers.Admin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        #region User Construtor
        public UserController(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;

        }
        #endregion
        #region User Login
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginModel user)
        {
            var userData = _userRepository.Login(user);
            if (userData != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ),
                    new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
					//new Claim("UserID", userData.UserID.ToString()),
					new Claim("UserName", userData.UserName.ToString()),
                    new Claim("Role", userData.Role.ToString()),

                    new Claim("Password", userData.Password.ToString()),

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: signIn
                    );

                string tockenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = tockenValue, User = userData, Message = "User Login Successfully" });
            }

            return BadRequest(new { Message = "Please enter valid Email and password" });
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
        #region User Register
        [HttpPost]
        public IActionResult Register([FromBody] UserRegistrationModel userRegistrationModel)
        {
            if (userRegistrationModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _userRepository.Register(userRegistrationModel);
            if (isInserted)
            {
                return Ok(new { Message = "User is register successfully" });
            }
            return StatusCode(500, "An error occured while registering");
        }
        #endregion
    }
}
