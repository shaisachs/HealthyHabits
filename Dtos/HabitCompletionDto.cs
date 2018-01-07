using System;
using HealthyHabits.Models;

namespace HealthyHabits.Dtos
{
    public class HabitCompletionDto : BaseDto<HabitCompletion>
    {
        public long HabitId { get; set; }
        public DateTime Completed { get; set; }
    }
}