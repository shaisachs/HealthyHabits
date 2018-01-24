using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace HealthyHabits.Validators
{
    [AttributeUsage(AttributeTargets.Property | 
    AttributeTargets.Field, AllowMultiple = false)]
    sealed public class NotFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            if (value.GetType() != typeof(DateTime))
            {
                return true;
            }

            if (((DateTime) value) <= DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string FormatErrorMessage(string name)
        {
        return String.Format(CultureInfo.CurrentCulture, "The {0} date must not be in the future", name);
        }        
    }
}
