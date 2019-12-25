using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChateeCore
{
    public class RegisterPasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool IsCorrect = (value as SecureString).Length >= 6;          
            return new ValidationResult(IsCorrect, "Password should be at least 6 characters!");
        }
    }
}
