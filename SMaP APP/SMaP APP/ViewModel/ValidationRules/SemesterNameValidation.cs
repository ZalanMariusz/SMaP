using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SMaP_APP.ViewModel.ValidationRules
{
    class SemesterNameValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string semesterName = (string)value;
            if (String.IsNullOrEmpty(semesterName))
            {
                return new ValidationResult(false, "The value is not a valid e-mail address");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
