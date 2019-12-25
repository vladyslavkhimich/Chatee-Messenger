using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChateeCore
{
    public class UsernameLengthValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool IsCorrect = (value as string).Length >= 5;
            return new ValidationResult(IsCorrect, "Username must contain at least 5 characters!");
        }
    }
}
