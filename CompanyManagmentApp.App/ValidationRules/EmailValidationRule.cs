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
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(Regex.Match(value as string, @"^\w+@\w+.\w+").Success)
            {
                return new ValidationResult(true, "Email must be in format: xxxx@xxxx.xxx");
            }
            else
            {
                return new ValidationResult(false, "Email must be in format: xxxx@xxxx.xxx");
            }
        }
    }
}
