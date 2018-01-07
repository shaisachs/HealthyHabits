using HealthyHabits.Models;

namespace HealthyHabits.Dtos
{
    public class HabitDto : BaseDto<Habit>
    {
        public string Name { get; set; }
    }
}