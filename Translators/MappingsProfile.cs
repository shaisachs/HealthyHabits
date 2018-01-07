using AutoMapper;
using HealthyHabits.Models;
using HealthyHabits.Dtos;

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
        }
    }
}
