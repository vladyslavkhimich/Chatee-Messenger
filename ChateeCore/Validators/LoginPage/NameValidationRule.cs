using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChateeCore
{
    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool IsCorrect = (string)value != string.Empty;
            return new ValidationResult(IsCorrect, "Field must not be empty!");
        }
    }
}
