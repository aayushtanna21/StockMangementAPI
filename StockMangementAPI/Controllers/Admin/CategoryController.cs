using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data.Admin;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers.Admin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #region Category GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var category = _categoryRepository.SelectAll();
            return Ok(category);
        }
        #endregion
        #region Category GetByID
        [HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var category = _categoryRepository.SelectByID(ID);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        #endregion
        #region Category Delete
        [HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var isDeleted = _categoryRepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
        #region Category GetInsert
        [HttpPost]
        public IActionResult Insert([FromBody] CategoryModel categoryModel)
        {
            if (categoryModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _categoryRepository.Insert(categoryModel);
            if (isInserted)
            {
                return Ok(new { Message = "Category is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
        #endregion
        #region Category Update
        [HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] CategoryModel categoryModel)
        {
            if (categoryModel == null || ID != categoryModel.CategoryID)
            {
                return BadRequest();
            }
            var isUpdated = _categoryRepository.Update(categoryModel);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
    }
}
