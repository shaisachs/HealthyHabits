using System;

namespace HealthyHabits.Models
{
    public class HabitCompletion : BaseModel
    {
        public long HabitId { get; set; }
        public DateTime Completed { get; set; }
    }
}