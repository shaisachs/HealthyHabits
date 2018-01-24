using System.Collections.Generic;
using HealthyHabits.Models;

namespace HealthyHabits.Dtos
{
    public class ValidationErrorDtoCollection
    {
        public IEnumerable<ValidationErrorDto> Items { get; private set; }

        public ValidationErrorDtoCollection(ValidationErrorDto error)
        {
            Items = new[] { error };
        }

         public ValidationErrorDtoCollection(IEnumerable<ValidationErrorDto> errors)
         {
             Items = errors;
         }
       
    }
}