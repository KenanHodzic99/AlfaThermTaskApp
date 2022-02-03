using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CompanyManagmentApp.App.ValidationRules
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int result = 0;
            bool canConvert = int.TryParse(value as string, out result);
            return new ValidationResult(canConvert, "Must be a number!");
        }
    }
}
