using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CompanyManagmentApp.App.ValidationRules
{
    public class EmptyStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(string.Empty == value as string || (value as string).Length < 3)
            {
                return new ValidationResult(false,"Cannot leave this field empty!");
            }
            else
            {
                return new ValidationResult(true, "Cannot leave this field empty!");
            }
        }
    }
}
