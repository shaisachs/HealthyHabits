using AutoMapper;
using HealthyHabits.Models;
using HealthyHabits.Dtos;
using HealthyHabits.Validators;

namespace HealthyHabits.Translators 
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Habit, HabitDto>()
                .ReverseMap();
            CreateMap<HabitCompletion, HabitCompletionDto>()
                .ReverseMap();
            CreateMap<ValidationError, ValidationErrorDto>()
                .ReverseMap();
        }
    }
}
