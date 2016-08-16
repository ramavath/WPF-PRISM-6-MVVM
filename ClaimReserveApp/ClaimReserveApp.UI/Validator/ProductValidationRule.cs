using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using ClaimReserveApp.DAL;

namespace ClaimReserveApp.UI.Validator
{
    public class ProductValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Product product = (value as BindingGroup).Items[0] as Product;
            if (Convert.ToInt32(product.OriginYear) > Convert.ToInt32(product.DevelopmentYear))
            {
                return new ValidationResult(false,
                    "Development year be after start origin year");
            }
            if (!ValidateYear(product.DevelopmentYear))
            {
                return new ValidationResult(false,
                    "Invalid Developemnt Year");
            }
            if (!ValidateYear(product.OriginYear))
            {
                return new ValidationResult(false,
                    "Invalid Origin Year");
            }
            if (product.IncreamentValue < 0)
            {
                return new ValidationResult(false,
                    "Increment is less than zero");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }

        private bool ValidateYear(string arg)
        {
            return (Regex.IsMatch(arg, "^(19|20)[0-9][0-9]"));
        }
    }
}
