using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;
        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
		#region Product GetAll
		[HttpGet]
        public IActionResult GetAll()
        {
            var products = _productRepository.SelectAll();
            return Ok(products);
        }
		#endregion
		#region Category DropDown
		[HttpGet]
        public IActionResult CategoryDropDown()
        {
            var category = _productRepository.CategoryDropDown();
            return Ok(category);
        }
		#endregion
		#region Customer DropDown
		[HttpGet]
        public IActionResult CustomerDropDown()
        {
            var user = _productRepository.CustomerDropDown();
            return Ok(user);
        }
		#endregion
		#region Product GetByID
		[HttpGet("{ID}")]
        public IActionResult GetByID(int ID)
        {
            var products = _productRepository.SelectByID(ID);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
		#endregion
		#region Product Delete
		[HttpDelete("{ID}")]
        public IActionResult Delete(int ID)
        {
            var isDeleted = _productRepository.Delete(ID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
		#endregion
		#region Product Insert
		[HttpPost]
        public IActionResult Insert([FromBody] ProductModel productModel)
        {
            if (productModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _productRepository.Insert(productModel);
            if (isInserted)
            {
                return Ok(new { Message = "Product is inserted successfully" });
            }
            return StatusCode(500, "An error occured while inserting");
        }
		#endregion
		#region Product Update
		[HttpPut("{ID}")]
        public IActionResult Update(int ID, [FromBody] ProductModel productModel)
        {
            if (productModel == null || ID != productModel.ProductID)
            {
                return BadRequest();
            }
            var isUpdated = _productRepository.Update(productModel);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
		#endregion
	}
}
