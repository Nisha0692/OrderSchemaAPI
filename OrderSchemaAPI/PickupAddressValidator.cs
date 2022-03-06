using FluentValidation;
using OrderSchemaAPI.Models;
using System.Text.RegularExpressions;

namespace OrderSchemaAPI
{
    public class PickupAddressValidator : AbstractValidator<PickupAddress> 
    {
        string regexPatternUnit = @"^0|[1-9][0-9]*$";
        string regexPatternStreet = @"^[A-Za-z_ ]{2,50}$";
        string regexPatternCity = @"^[A-Za-z]{2,20}$";
        string regexPatternSuburb = @"^[A-Za-z_ ]{2,20}$";
        string regexPatternPostCode = @"^[0-9]{4}$";
        public PickupAddressValidator()
        {
            RuleFor(x => x.Unit).NotEmpty().Must(BeAValidUnit).WithMessage("Please specify a valid unit");
            RuleFor(x => x.Street).NotEmpty().Must(BeAValidStreet).WithMessage("Please specify a valid street name");
            RuleFor(x => x.City).NotEmpty().Must(BeAValidCity).WithMessage("Please specify a valid city");
            RuleFor(x => x.Suburb).NotEmpty().Must(BeAValidSuburb).WithMessage("Please specify a valid suburb");
            RuleFor(x => x.Postcode).NotEmpty().Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
        }

        private bool BeAValidUnit(int unit)
        {
            var strUnit = unit.ToString();
            Match match = Regex.Match(strUnit,regexPatternUnit);
            if(match.Success)
            {
                return true;
            }
            return false;
        }

        private bool BeAValidStreet(string street)
        {
            Match match = Regex.Match(street, regexPatternStreet);
            if (match.Success)
            {
                return true;
            }
            return false;
        }

        private bool BeAValidCity(string city)
        {
            Match match = Regex.Match(city, regexPatternCity);
            if (match.Success)
            {
                return true;
            }
            return false;
        }

        private bool BeAValidSuburb(string suburb)
        {
            Match match = Regex.Match(suburb, regexPatternSuburb);
            if (match.Success)
            {
                return true;
            }
            return false;
        }

        private bool BeAValidPostcode(int post)
        {
            var strPost = post.ToString();
            Match match = Regex.Match(strPost, regexPatternPostCode);
            if (match.Success)
            {
                return true;
            }
            return false;
        }
    }
}
