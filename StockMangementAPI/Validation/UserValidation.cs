using FluentValidation;
using StockMangementAPI.Models;

namespace StockMangementAPI.Validation
{
    public class UserValidation: AbstractValidator<UserModel>
    {
		#region User Validation
		public UserValidation()
        {
            //RuleFor(u=>u.UserID).NotEmpty();
            RuleFor(u=>u.UserName).NotEmpty().WithMessage("UserName required")
                .Length(1,20).WithMessage("Length should be between 1 and 20");
            RuleFor(u => u.Password).NotNull().WithMessage("Password is requried")
                .NotEmpty().WithMessage("Password should not be empty")
                .Length(4, 50).WithMessage("Password should be between 4 to 50");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email requires")
                .EmailAddress();
            RuleFor(u => u.PhoneNumber).NotEmpty().WithMessage("Phone Number requried")
                .Length(1, 10).WithMessage("Length should be between 1 to 10");
            //RuleFor(u => u.Created).NotEmpty().WithMessage("Created date should not be Empty");
            //RuleFor(u => u.Modified).NotEmpty().WithMessage("Modified date should not be Empty");
        }
		#endregion
	}
}
