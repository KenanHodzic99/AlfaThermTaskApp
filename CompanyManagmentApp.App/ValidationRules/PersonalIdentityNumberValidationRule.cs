using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AlfaThermTaskApp.App.ValidationRules
{
    public class PersonalIdentityNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int result = 0;
            bool canConvert = int.TryParse(value as string, out result);
            if (canConvert)
            {
                if ((value as string).Length > 12)
                {
                    return new ValidationResult(canConvert, "Must be a number and must have 13 digits!");
                }
                else
                {
                    return new ValidationResult(false, "Must be a number and must have 13 digits!");
                }
            }
            else {
                return new ValidationResult(canConvert, "Must be a number and must have 13 digits!");
            }
        }
    }
}
