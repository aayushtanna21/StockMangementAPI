using FluentValidation;
using StockMangementAPI.Models;

namespace StockMangementAPI.Validation
{
    public class CategoryValidation: AbstractValidator<CategoryModel>
    {
		#region Category Validation
		public CategoryValidation()
        {
            //RuleFor(u=>u.CategoryID).NotEmpty().WithMessage("Category ID is requried");
            RuleFor(u=>u.CategoryName).NotEmpty().WithMessage("UserName required")
                .Length(1,20).WithMessage("Length should be between 1 and 20");
            //RuleFor(u => u.Created).NotEmpty().WithMessage("Created date should not be Empty");
           // RuleFor(u => u.Modified).NotEmpty().WithMessage("Modified date should not be Empty");
        }
		#endregion
	}
}
