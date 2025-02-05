using FluentValidation;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using StockMangementAPI.Models;

namespace StockMangementAPI.Validation
{
    public class StockTransactionValidation : AbstractValidator<StockTransactionsModel>
    {
		#region StockTransaction Validation
		public StockTransactionValidation()
        {
            //RuleFor(u=>u.StockTransactionID).NotEmpty().WithMessage("Supplier ID is required");
            RuleFor(u => u.ProductID).NotEmpty().WithMessage("Product ID is required");
            RuleFor(u=>u.StockTransactionType).NotEmpty().WithMessage("StockTransactionType required")
                .MaximumLength(50).WithMessage("StockTransactionType should be less than 50 ");
            RuleFor(u => u.Quantity).NotEmpty().WithMessage("Quantity requried")
                .NotEqual(0).WithMessage("Quantity should not be 0");
            RuleFor(u => u.StockTransactionDate).NotNull().WithMessage("StockTransactionDate is requried")
                .NotEmpty().WithMessage("Address should not be empty");
            RuleFor(u => u.UserID).NotEmpty().WithMessage("UserID is required");
           // RuleFor(u => u.Modified).NotEmpty().WithMessage("Modified date should not be Empty");
        }
		#endregion
	}
}