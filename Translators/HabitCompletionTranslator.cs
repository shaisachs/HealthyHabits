using HealthyHabits.Models;
using HealthyHabits.Dtos;
using AutoMapper;

namespace HealthyHabits.Translators
{
    public class HabitCompletionTranslator : BaseTranslator<HabitCompletion, HabitCompletionDto> 
    {
        public HabitCompletionTranslator(IMapper mapper) : base(mapper) { }
    }
}
