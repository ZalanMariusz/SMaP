using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SMaP_APP.ViewModel.ValidationRules
{
    class SemesterTypeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value==null || int.Parse(value.ToString()) == 0)
            {
                return new ValidationResult(false, "Szemeszter típusa nem lehet üres!");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
