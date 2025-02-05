using FluentValidation;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using StockMangementAPI.Models;

namespace StockMangementAPI.Validation
{
    public class ProductValidation : AbstractValidator<ProductModel>
    {
		#region Product Validation
		public ProductValidation()
        {
           // RuleFor(u=>u.ProductID).NotEmpty().WithMessage("Product ID is required");
            RuleFor(u => u.ProductName).NotEmpty().WithMessage("ProductName required")
                .MaximumLength(50).WithMessage("ProductName should be less than 50 ");
            RuleFor(u => u.CategoryID).NotEmpty().WithMessage("Category ID is required");
            RuleFor(u => u.StockQuantity).NotEmpty().WithMessage("StockQuantity is required");
            RuleFor(u => u.Price).NotEmpty().WithMessage("Price requried")
                .NotEqual(0).WithMessage("Price Should not be 0");
            RuleFor(u => u.CostPrice).NotEmpty().WithMessage("CostPrice requires");
            RuleFor(u => u.Unit).NotNull().WithMessage("Unit is requried")
                .NotEmpty().WithMessage("Address should not be empty");
            RuleFor(u => u.CustomerID).NotEmpty().WithMessage("CustomerID is required");
            //RuleFor(u => u.Created).NotEmpty().WithMessage("Created date should not be Empty");
            //RuleFor(u => u.Modified).NotEmpty().WithMessage("Modified date should not be Empty");
        }
		#endregion
	}
}