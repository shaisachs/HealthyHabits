using Microsoft.EntityFrameworkCore;

namespace HealthyHabits.Models
{
    public class HealthyHabitsContext : DbContext
    {
        public HealthyHabitsContext(DbContextOptions<HealthyHabitsContext> options)
            : base(options)
        {
        }

    }
}