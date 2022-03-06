using FluentValidation;
using OrderSchemaAPI.Models;

namespace OrderSchemaAPI
{
    public class ItemValidator : AbstractValidator<Item>
    {
        string regexPatternItemCode = @"^[0-9A-Z]+(-[0-9A-Z]+)+$";
        
        public ItemValidator()
        {
            RuleFor(x => x.ItemCode).NotNull().Matches(regexPatternItemCode).WithMessage("Please provide a valid item code");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Please provide numeric value");
        }
    }
}
