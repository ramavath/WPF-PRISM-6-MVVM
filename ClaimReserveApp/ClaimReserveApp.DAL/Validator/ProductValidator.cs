using System;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ClaimReserveApp.DAL.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
       
        public ProductValidator()
        {

            When(product => product != null, () => {

                RuleFor(product => product.ProductName)
                .NotEmpty()
                .WithMessage("Product name is Blank");

                RuleFor(product => product.OriginYear).NotEmpty()
                .NotEmpty()
                .WithMessage("Origin year is Blank");
                RuleFor(product => product.DevelopmentYear).NotEmpty()
                .NotEmpty()
                .WithMessage("DevelopmentYear year is Blank");

             RuleFor(product => product.IncreamentValue).NotEmpty()
             .NotEmpty()
             .WithMessage("Increament Value is Blank");

                RuleFor(product => product.OriginYear)
                .Must(ValidateYear)
                .WithMessage("Invalid Origin Year");


                RuleFor(product => product.DevelopmentYear)
                .Must(ValidateYear)
                .WithMessage("Invalid Development Year");

                RuleFor(x => x.DevelopmentYear)
              .GreaterThanOrEqualTo(x => x.OriginYear)
              .WithMessage("Development year must be greater than or equal origin year");

            });
        }

        private bool CompareYear(Product arg)
        {
            return Convert.ToInt32(arg.DevelopmentYear) >= Convert.ToInt32(arg.OriginYear);
        }

        private bool ValidateYear(string arg)
        {
            return (Regex.IsMatch(arg, "^(19|20)[0-9][0-9]"));
        }
    }
}
