using HealthyHabits.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthyHabits.Repositories
{
    public class HabitCompletionRepository : BaseRepository<HabitCompletion>
    {
        public HabitCompletionRepository(HealthyHabitsContext context) : base(context) { }

        protected override DbSet<HabitCompletion> _dbset
        {
            get
            {
                return _context.HabitCompletions;
            }
        }
    }
}