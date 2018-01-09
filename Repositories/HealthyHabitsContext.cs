using Microsoft.EntityFrameworkCore;
using HealthyHabits.Models;

namespace HealthyHabits.Repositories
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