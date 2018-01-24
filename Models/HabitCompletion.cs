using System;
using System.ComponentModel.DataAnnotations;
using HealthyHabits.Validators;

namespace HealthyHabits.Models
{
    public class HabitCompletion : BaseModel
    {
        public long HabitId { get; set; }

        [NotFutureDate]
        public DateTime Completed { get; set; }
    }
}