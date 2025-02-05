using FluentValidation;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using StockMangementAPI.Models;

namespace StockMangementAPI.Validation
{
    public class BillsValidation : AbstractValidator<BillsModel>
    {
		#region Bills Validation
		public BillsValidation()
        {
           // RuleFor(u=>u.BillID).NotEmpty().WithMessage("Bill ID is required");
            RuleFor(u => u.TotalAmount).NotEmpty().WithMessage("TotalAmount required");
            RuleFor(u => u.CustomerID).NotEmpty().WithMessage("Customer ID is required");
            RuleFor(u => u.PaymentMode).NotEmpty().WithMessage("PaymentMode is required");
            RuleFor(u => u.UserID).NotEmpty().WithMessage("UserID is required");
           // RuleFor(u => u.BillDate).NotEmpty().WithMessage("Bill date should not be Empty");
           // RuleFor(u => u.Modified).NotEmpty().WithMessage("Modified date should not be Empty");
        }
		#endregion
	}
}