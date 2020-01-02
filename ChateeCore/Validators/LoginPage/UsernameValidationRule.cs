using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChateeCore
{
    public class UsernameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex emailRegex = new Regex(@"^[a-zA-Z][a-zA-Z0-9]{4,11}$");
            Match match = emailRegex.Match(value as string);            
            return new ValidationResult(match.Success, "Email is not valid!");
        }
    }
}
