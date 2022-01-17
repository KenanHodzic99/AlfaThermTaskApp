using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AlfaThermTaskApp.App.ValidationRules
{
    public class PhoneNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(Regex.Match(value as string, @"^\d{3}-\d{3}-\d{3}").Success)
            {
                return new ValidationResult(true, "Phone number must be in format XXX-XXX-XXX");
            }
            else
            {
                return new ValidationResult(false, "Phone number must be in format XXX-XXX-XXX");
            }
        }
    }
}
