﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data.Admin;
using StockMangementAPI.Data.User;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserSalesController : ControllerBase
    {
        private readonly UserSalesRepository _usersalesrepository;
        public UserSalesController(UserSalesRepository usersalesrepository)
        {
            _usersalesrepository = usersalesrepository;
        }
        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var stockTransactions = _usersalesrepository.SelectAll();
            return Ok(stockTransactions);
        }
        #endregion
        #region Product DropDown
        [HttpGet]
        public IActionResult ProductDropDown()
        {
            var stockTransactions = _usersalesrepository.ProductDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region Customer DropDown
        [HttpGet]
        public IActionResult CustomerDropDown()
        {
            var stockTransactions = _usersalesrepository.CustomerDropDown();
            return Ok(stockTransactions);
        }
        #endregion
        #region  GetByID
        [HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var stockTransactions = _usersalesrepository.SelectByID(ID);
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
            var isDeleted = _usersalesrepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
        #region  Insert

        [HttpPost]
        public IActionResult Insert([FromBody] UserSalesModel userSalesModel)
        {
            if (userSalesModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _usersalesrepository.Insert(userSalesModel);
            if (isInserted)
            {
                return Ok(new { Message = "StockTransactions is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
        #endregion
        #region  Update
        [HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] UserSalesModel userSalesModel)
        {
            if (userSalesModel == null || ID != userSalesModel.SalesID)
            {
                return BadRequest();
            }
            var isUpdated = _usersalesrepository.Update(userSalesModel);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion
    }
}
