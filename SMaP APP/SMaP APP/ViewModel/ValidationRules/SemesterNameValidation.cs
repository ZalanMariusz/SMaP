﻿using System;
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
            if (String.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Szemeszter neve nem lehet üres!");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
