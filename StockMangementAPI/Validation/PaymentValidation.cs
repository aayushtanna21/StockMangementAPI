using FluentValidation;
using StockMangementAPI.Models;

namespace StockMangementAPI.Validation
{
    public class PaymentValidation : AbstractValidator<PaymentsModel>
    {
		#region Payment Validation
		public PaymentValidation()
        {
            //RuleFor(u=>u.PaymentID).NotEmpty().WithMessage("Payment ID is requried");
            RuleFor(u => u.BillID).NotEmpty().WithMessage("Bill ID is requried");
            RuleFor(u => u.PaymentMode).NotEmpty().WithMessage("PaymentMode is required");
            RuleFor(u => u.AmountPaid).NotNull().WithMessage("AmountPaid is requried")
                .NotEmpty().WithMessage("AmountPaid should not be empty");
            //RuleFor(u => u.PaymentDate).NotEmpty().WithMessage("Payment date should not be Empty");
        }
		#endregion
	}
}
