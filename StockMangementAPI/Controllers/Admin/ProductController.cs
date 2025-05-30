using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StockMangementAPI.Data.Admin;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Controllers.Admin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;
        private readonly IConfiguration _configuration;
        public ProductController(ProductRepository productRepository, IConfiguration configuration)
        {
            _productRepository = productRepository;
            _configuration = configuration;
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
        public IActionResult Insert([FromForm] ProductModel productModel)
        {
            if (productModel == null)
            {
                return BadRequest();
            }
            if (productModel.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages");
                Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productModel.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productModel.ImageFile.CopyTo(fileStream);
                }

                productModel.ProductImage = "/ProductImages/" + uniqueFileName; // Return URL instead of Base64
            }


            bool isInserted = _productRepository.Insert(productModel);
            if (isInserted)
            {
                return Ok(new { Message = "Product inserted successfully!", Product = productModel });
            }
            return StatusCode(500, "Error inserting product.");
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
        //#region Product Filter
        //[HttpGet]
        //public IActionResult GetProductFilter(string? categoryName = null, decimal? price = null, string? customerName = null)
        //{
        //    var products = _productRepository.GetFilteredProducts(categoryName, price, customerName);
        //    return Ok(products);
        //}

        //#endregion
        #region Price DropDown
        [HttpGet]
        public IActionResult PriceDropDown()
        {
            var prices = _productRepository.GetPriceDropDown();
            return Ok(prices);
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> GetCategoryNames()
        {
            var categories = await _productRepository.GetCategoryNames();
            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetDistinctPrices()
        {
            var prices = await _productRepository.GetDistinctPrices();
            return Ok(prices);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerNames()
        {
            var customers = await _productRepository.GetCustomerNames();
            return Ok(customers);
        }

        [HttpGet]
        public IActionResult FilterProducts(int? categoryID, decimal? price, int? customerID, DateTime? createdDate)
        {
            var products = _productRepository.FilterProducts(categoryID, price, customerID, createdDate);
            return Ok(products);
        }

    }
}
