using HealthyHabits.Models;
using HealthyHabits.Dtos;
using AutoMapper;
using HealthyHabits.Validators;

namespace HealthyHabits.Translators
{
    public class ValidationErrorTranslator
    {
        private readonly IMapper _mapper;

        public ValidationErrorTranslator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ValidationErrorDto Translate(ValidationError error)
        {
            return _mapper.Map<ValidationErrorDto>(error);
        }

    }
}
