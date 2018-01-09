using HealthyHabits.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthyHabits.Repositories
{
    public class HabitRepository : BaseRepository<Habit>
    {
        public HabitRepository(HealthyHabitsContext context) : base(context) { }

        protected override DbSet<Habit> _dbset
        {
            get
            {
                return _context.Habits;
            }
        }
    }
}