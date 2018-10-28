using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SMaP_APP.ViewModel.ValidationRules
{
    class NeptunLengthValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!String.IsNullOrEmpty((string)value) && ((string)value).Length!=6)
            {
                return new ValidationResult(false, "A NEPTUN-kód hossza 6 karakter!");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
