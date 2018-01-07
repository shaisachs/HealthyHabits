using HealthyHabits.Models;
using HealthyHabits.Dtos;
using AutoMapper;

namespace HealthyHabits.Translators
{
    public class HabitTranslator : BaseTranslator<Habit, HabitDto> 
    {
        public HabitTranslator(IMapper mapper) : base(mapper) { }
    }
}
