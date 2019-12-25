using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChateeCore
{
    public class EmailExistenceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool IsCorrect = IoCContainer.Get<ApplicationViewModel>().ServiceClient.CheckEmailDatabaseExistence(value as string);
            return new ValidationResult(!IsCorrect, "User with this e-mail already exists");
        }
    }
}
