using FluentValidation;
using OrderSchemaAPI.Models;

namespace OrderSchemaAPI
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.RequestedPickupTime).NotEmpty().Must(BeAValidDate);
            RuleFor(x => x.PickupInstructions).NotEmpty().Length(0, 250);
            RuleFor(x => x.DeliveryInstructions).NotEmpty().Length(0, 250);
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}