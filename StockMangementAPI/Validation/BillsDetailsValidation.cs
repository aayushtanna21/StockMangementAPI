using FluentValidation;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using StockMangementAPI.Models;

namespace StockMangementAPI.Validation
{
    public class BillsDetailsValidation : AbstractValidator<BillDetailsModel>
    {
		#region BillsDetails Validation
		public BillsDetailsValidation()
        {
            RuleFor(u=>u.BillID).NotEmpty().WithMessage("Bill ID is required");
            //RuleFor(u => u.BillDetailID).NotEmpty().WithMessage("BillDetailID required");
            RuleFor(u => u.ProductID).NotEmpty().WithMessage("Product ID is required");
            RuleFor(u => u.Quantity).NotEmpty().WithMessage("Quantity is required")
                .NotEqual(0).WithMessage("Quantity should not be 0");
            RuleFor(u => u.UnitPrice).NotEmpty().WithMessage("UnitPrice is required");
            RuleFor(u => u.SubTotal).NotEmpty().WithMessage("SubTotal should not be Empty");
        }
		#endregion
	}
}