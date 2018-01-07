using Microsoft.EntityFrameworkCore;

namespace HealthyHabits.Models
{
    public class HealthyHabitsContext : DbContext
    {
        public HealthyHabitsContext(DbContextOptions<HealthyHabitsContext> options)
            : base(options)
        {
        }

        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitCompletion> HabitCompletions { get; set; }
    }
}