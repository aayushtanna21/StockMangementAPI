using FluentValidation;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using StockMangementAPI.Models;

namespace StockMangementAPI.Validation
{
    public class SuppliersValidation : AbstractValidator<SuppliersModel>
    {
		#region Suppliers Validation
		public SuppliersValidation()
        {
           // RuleFor(u=>u.SupplierID).NotEmpty().WithMessage("Supplier ID is required");
            RuleFor(u => u.SupplierName).NotEmpty().WithMessage("SupplierName required")
                .MaximumLength(50).WithMessage("SupplierName should be less than 50 ");
            RuleFor(u => u.PhoneNumber).NotEmpty().WithMessage("Phone Number requried")
                .Length(1, 10).WithMessage("Length should be between 1 to 10");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email requires")
               .EmailAddress();
            RuleFor(u => u.Address).NotNull().WithMessage("Address is requried")
                .NotEmpty().WithMessage("Address should not be empty");
            RuleFor(u => u.UserID).NotEmpty().WithMessage("UserID is required");
            //RuleFor(u => u.Created).NotEmpty().WithMessage("Created date should not be Empty");
           // RuleFor(u => u.Modified).NotEmpty().WithMessage("Modified date should not be Empty");
        }
		#endregion
	}
}