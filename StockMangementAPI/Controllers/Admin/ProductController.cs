using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMangementAPI.Data.Admin;
using StockMangementAPI.Models;

namespace StockMangementAPI.Controllers.Admin
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
        //#region Upload
        //[HttpPost("UploadImage")]
        //[Consumes("multipart/form-data")]
        //public IActionResult UploadImage([FromForm] IFormFile imageFile)
        //{
        //	if (imageFile == null || imageFile.Length == 0)
        //	{
        //		return BadRequest("No image file uploaded.");
        //	}

        //	string imagePath = ImageHelper.SaveImageToFile(imageFile);
        //	if (string.IsNullOrEmpty(imagePath))
        //	{
        //		return StatusCode(500, "Image upload failed.");
        //	}

        //	return Ok(imagePath);
        //}

        //#endregion
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
                productModel.ProductImage = ImageHelper.ConvertImageToBase64(productModel.ImageFile);
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

    }
}
